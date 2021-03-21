# ThaiNationalIDCard

A fork of chakphanu/ThaiNationalIDCard and maintained by .NET Thailand group

## About the project 

จุดกำเนิดของ library ตัวนี้ มาจากตอนที่คุณ Chakphanu Komasathit ยังทำงานที่ ม.ราชภัฏ
ตอนนั้นต้องการให้น้องๆ programmer สร้าง application เพื่อดึงข้อมูลจากบัตรประจำตัวประชาชน
แต่ ณ เวลานั้น library ต่างๆ ต้องซื้อและมี license ต่อเครื่อง ไม่ได้มี open source ให้ใช้งานได้ฟรี

จนคุณ Chakphanu Komasathit ได้ไปเจอกับ APDU command Delphi code ของคุณ Manoi ที่ forum hosxp เลยเริ่มนำมาพัฒนาเป็น C# .NET library 

ซึ่งตอนนั้นความรู้ smartcard ของคุณ Chakphanu Komasathit เป็นศูนย์ คือต้องเริ่มนับหนึ่งใหม่
การพัฒนาจำเป็นต้องใช้  Wireshark เพื่อดูข้อมูลที่แลกเปลี่ยนกันผ่านทาง USB
จนในที่สุดเมื่อได้ library ที่สามารถใช้งานได้ จึงได้ส่งต่อให้น้องๆ ในที่ทำงานนำไปพัฒนา application ต่อไป

## Code example

``` CSharp
using ThaiNationalIDCard;
...
...
public void readCard()
{
	var idcard = new ThaiIDCard();
	var personal = idcard.readAll();
	if (personal != null)
	{
		Console.WriteLine(personal.Citizenid);
		Console.WriteLine(personal.Birthday?.ToString("dd/MM/yyyy"));
		Console.WriteLine(personal.Sex);
		Console.WriteLine(personal.Th_Prefix);
		Console.WriteLine(personal.Th_Firstname);
		Console.WriteLine(personal.Th_Lastname);
		Console.WriteLine(personal.En_Prefix);
		Console.WriteLine(personal.En_Firstname);
		Console.WriteLine(personal.En_Lastname);
		Console.WriteLine(personal.Issue.ToString("dd/MM/yyyy")); // วันออกบัตร
		Console.WriteLine(personal.Expire.ToString("dd/MM/yyyy")); // วันหมดอายุ

		Console.WriteLine(personal.Address);
		Console.WriteLine(personal.addrHouseNo); // บ้านเลขที่
		Console.WriteLine(personal.addrVillageNo); // หมู่ที่
		Console.WriteLine(personal.addrLane); // ซอย
		Console.WriteLine(personal.addrRoad); // ถนน
		Console.WriteLine(personal.addrTambol);
		Console.WriteLine(personal.addrAmphur);
		Console.WriteLine(personal.addrProvince);
	}
	else if (idcard.ErrorCode() > 0)
	{
		Console.WriteLine(idcard.Error());
	}
}            

```

## Credit
Original repository by Mr. Chakphanu Komasathit https://github.com/chakphanu/ThaiNationalIDCard
APDU Command from Mr. Manoi http://hosxp.net/index.php?option=com_smf&topic=22496
3B 78 Type of thai smartcard See on wiki. https://github.com/kolry/ThaiNationalIDCard/wiki/_pages

## Todo 

- [ ] Convet to .NET Standard
- [ ] Add some unit test
- [ ] GitHub Actions workflow to run unit test
- [ ] GitHub Actions workflow to deploy as nuget package
- [ ] Cross platform client app
- [ ] Blazor app

