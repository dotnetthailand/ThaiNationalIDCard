﻿/* BSD license
 * Credit:  APDU Command from  Mr.Manoi http://hosxp.net/index.php?option=com_smf&topic=22496
 */

using System;
using System.Linq;
using System.Windows.Media.Imaging;
using System.IO;

namespace ThaiNationalIDCard
{
    public class Personal
    {
        private string _personal;
        private string _address;
        private string _issue_expire;
        private string _issuer;
        private string[] _th_personal;
        private string[] _en_personal;

        private byte[] _photo_jpeg;

        public string Citizenid { get; set; }
        public string NumberUnderImg { get; set; }

        public byte[] PhotoRaw
        {
            get => _photo_jpeg;
            set => _photo_jpeg = value;
        }

        public System.Drawing.Bitmap PhotoBitmap
        {
            get
            {
                if (_photo_jpeg == null)
                    return null;
                JpegBitmapDecoder decoder = new JpegBitmapDecoder(new MemoryStream(_photo_jpeg), BitmapCreateOptions.PreservePixelFormat, BitmapCacheOption.Default);
                BitmapSource bitmapSource = decoder.Frames[0];
                using (System.IO.MemoryStream outStream = new System.IO.MemoryStream())
                {
                    BitmapEncoder enc = new BmpBitmapEncoder();
                    enc.Frames.Add(BitmapFrame.Create(bitmapSource));
                    enc.Save(outStream);
                    return new System.Drawing.Bitmap(outStream);
                }
            }
        }

        public string Info
        {
            set
            {
                _personal = value;
                _th_personal = _personal.Substring(0, 100).Split('#');
                _en_personal = _personal.Substring(100, 100).Split('#');
            }
        }

        public string Address
        {
            set => _address = value.Trim();
            get => _address.Replace('#', ' ');
        }

        public string addrHouseNo => _address.Split('#')[0].Trim();

        public string addrVillageNo => _address.Split('#')[1].Trim();

        public string addrLane => _address.Split('#')[2].Trim();

        public string addrRoad => _address.Split('#')[3].Trim();

        public string addrTambol => _address.Split('#')[5].Trim();

        public string addrAmphur => _address.Split('#')[6].Trim();

        public string addrProvince => _address.Split('#')[7].Trim();

        public string Issue_Expire
        {
            set => _issue_expire = value;
        }

        public DateTime Issue => new DateTime(
            Convert.ToInt32(_issue_expire.Substring(0, 4)) - 543,
            Convert.ToInt32(_issue_expire.Substring(4, 2)),
            Convert.ToInt32(_issue_expire.Substring(6, 2))
        );

        public DateTime Expire
        {
            get
            {
                var year = Convert.ToInt32(_issue_expire.Substring(8, 4)) - 543;
                var month = Convert.ToInt32(_issue_expire.Substring(12, 2));
                var day = Convert.ToInt32(_issue_expire.Substring(14, 2));

                return new DateTime(year, month > 12 ? 12 : month, day > 31 ? 31 : day);
            }
        }

        public string ExpireString => Expire.ToString("yyyyMMdd");

        // Update Language version https://stackoverflow.com/a/60378136/1872200
        public DateTime? Birthday
        {
            get
            {
                var year = Convert.ToInt32(_personal.Substring(200, 4));
                var month = Convert.ToInt32(_personal.Substring(204, 2));
                var day = Convert.ToInt32(_personal.Substring(206, 2));

                return new[] { year, month, day }.All(v => v > 0)
                    ? new DateTime(year - 543, month, day)
                    : null;
            }
        }

        public string BirthdayYearString => (Convert.ToInt32(_personal.Substring(200, 4)) - 543).ToString();

        public string Sex => _personal.Substring(208, 1);

        public string Th_Prefix => _th_personal[0].Trim();

        public string Th_Firstname => _th_personal[1].Trim();

        public string Th_Middlename => _th_personal[2].Trim();

        public string Th_Lastname => _th_personal[3].Trim();

        public string En_Prefix => _en_personal[0].Trim();

        public string En_Firstname => _en_personal[1].Trim();
        public string En_Middlename => _en_personal[2].Trim();

        public string En_Lastname => _en_personal[3].Trim();

        public string Issuer
        {
            get => _issuer;
            set => _issuer = value;
        }
    }
}

