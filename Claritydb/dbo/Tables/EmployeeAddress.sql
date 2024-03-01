CREATE TABLE [dbo].[EmployeeAddress]
(
	[EmployeeAddressId] bigint not null primary key identity(1,1),
	[EmployeeId] bigint null,
	[HNo] varchar(max) null,
	[AddressLineOne] varchar(max) null,
	[AddressLineTwo] varchar(max) null,
	[Landmark]  varchar(max) null,
	[CityId] bigint null,
	[StateId] bigint null,
	[CountryId] bigint null,
	[Zipcode] varchar(max) null,
	[CreatedOn] datetimeoffset null,
	[CreatedBy] bigint null,
	[ModifiedOn] datetimeoffset null,
	[ModifiedBy] bigint null,
	[IsActive] bit null,
	FOREIGN KEY (EmployeeId) REFERENCES Employee(EmployeeId),
	FOREIGN KEY (CityId) REFERENCES City(Id),
	FOREIGN KEY (StateId) REFERENCES State(StateId),
	FOREIGN KEY (CountryId) REFERENCES Country(Id)
)
