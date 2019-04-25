Design Doc - note github +01 gmail

SUMMARY OF FUNCTION

will be an api for updating a database about game price information. have CRUD functionality. 
	TABLES:
		-GAMES : id, game, desired price
			- only need to set id, game & desired price on creation
			- will be able to add, update and delete this via api as well as Get
		-ITEMS :id, GAME, source, link, currentPrice, dateUpdated, comments, endDate
			- on creation only need id, GAME, source, link, currentPrice
			- store links will update
			- ebay links will have expired info
			- ebay links will keep being added
			- so will be able to update store links for price changes and dates
			- comments reserving in case need any notes
			- ebay links will have a endDate so can know end

Once working will consume on a frontend to add data (new game) or delete. also can update game info
frontend will also be able to delete items and add comments.

TESTS
naming convention : 
test_ MethodName_StateUnderTest_ExpectedBehavior: 

e.g. test_isAdult_AgeLessThan18_False

TECH USING

SQL, Entityframework, WEB API, Auth, Git, Testing framework (Nunit or something else) 

CURRENT TODO

create tables in SQL - done
create entity data layer - done
create controllers - done
create Authentication - basic, need token setup
create logger
write unit tests

LOGGING

log to database

ACCOMPOLISHMENTS

BUGS

FUTURE FEATURES
