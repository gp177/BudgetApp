Select Records.RecordId,Accounts.AccountName,Category.CategoryType,
stuff(
    (
    select cast(',' as varchar(max)) + 
	 tag.Description
     from Records 
  Inner Join Accounts  
  On Records.AccountId = Accounts.AccountId 
   Inner Join Category 
  On Records.CategoryId = Category.CategoryId 
  Inner Join InterTag 
   On Records.RecordId = InterTag.RecordId 
 Inner Join Tag 
 On InterTag.TagId = Tag.TagId
 
    for xml path('')
    ), 1, 1, '') AS Description

, Records.Date,Records.Amount,Records.RecordType 
from Records 
Inner Join Accounts  
On Records.AccountId = Accounts.AccountId 
Inner Join Category 
On Records.CategoryId = Category.CategoryId 
Inner Join InterTag 
On Records.RecordId = InterTag.RecordId 
Inner Join Tag 
On InterTag.TagId = Tag.TagId

 group by Records.RecordId,Accounts.AccountName,Category.CategoryType, Records.Date,Records.Amount,Records.RecordType 