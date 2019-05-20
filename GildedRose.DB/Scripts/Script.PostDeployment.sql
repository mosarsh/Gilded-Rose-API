/*
Post-Deployment Script Template							
--------------------------------------------------------------------------------------
 This file contains SQL statements that will be appended to the build script.		
 Use SQLCMD syntax to include a file in the post-deployment script.			
 Example:      :r .\myfile.sql								
 Use SQLCMD syntax to reference a variable in the post-deployment script.		
 Example:      :setvar TableName MyTable							
               SELECT * FROM [$(TableName)]					
--------------------------------------------------------------------------------------
*/
USE [GildedRose.DB];
GO

-- Popultate Inventory Table
IF OBJECT_ID(N'dbo.InventoryType', N'U') IS NOT NULL
BEGIN
    PRINT 'Started Populating InventoryType Table'

	MERGE INTO dbo.[InventoryType] AS Target
	USING(VALUES (1, 'Ice Cream Bar - Oreo Sandwich')
	,(2, 'Ranchero - Primerba, Paste')
	,(3, 'Sauce - Roasted Red Pepper')
	,(4, 'Muffin Batt - Choc Chk')
	,(5, 'Venison - Ground')
	,(6, 'Wine - Magnotta - Red, Baco')
	,(7, 'Zucchini - Yellow')
	,(8, 'Lettuce - Frisee')
	,(9, 'Calvados - Boulard')
	,(10, 'Sobe - Lizard Fuel')) AS Source(Id, Name)
   ON Target.Id = Source.Id 
   WHEN MATCHED then 
   UPDATE SET 
   Target.Name=Source.Name
   WHEN NOT MATCHED THEN 
   INSERT VALUES(Source.Name); 

   PRINT 'Finished Populating InventoryType Table'
END

GO

IF OBJECT_ID(N'dbo.Inventory', N'U') IS NOT NULL
BEGIN
    PRINT 'Started Populating Inventory Table'

	MERGE INTO dbo.[Inventory] AS Target
	USING(VALUES (1, 9, 'Chips - Miss Vickies', 'Sltr-haris Type II physl fx low end unsp femr, 7thD', 83.66, 5, '2018-10-29T12:21:08Z', '2018-07-29T01:19:13Z')
	,(2, 1, 'Ice Cream Bar - Drumstick', 'Hemorrhage due to genitourinary prosth dev/grft, subs', 30.46, 6, '2018-07-06T02:15:22Z', '2018-05-27T03:17:18Z')
	,(3, 9, 'Pie Filling - Pumpkin', 'Osseous stenosis of neural canal of sacral region', 74.24, 7, '2018-11-13T22:33:53Z', '2018-11-15T09:07:21Z')
	,(4, 7, 'Beans - French', 'Inj flexor musc/fasc/tend unsp finger at wrs/hnd lv, subs', 12.57, 12, '2018-11-16T22:01:43Z', '2019-02-18T14:44:07Z')
	,(5, 3, 'Momiji Oroshi Chili Sauce', 'Underdosing of methylphenidate, initial encounter', 46.83, 14, '2018-12-29T16:26:41Z', '2018-09-21T18:08:44Z')
	,(6, 7, 'Roe - White Fish', 'Person on outside of car injured in collision w car in traf', 81.34, 56, '2019-02-25T18:23:52Z', '2018-07-16T05:34:34Z')
	,(7, 3, 'Jagermeister', 'Accidental kick by another person, initial encounter', 68.9, 43, '2018-10-16T21:21:40Z', '2019-03-16T04:26:01Z')
	,(8, 1, 'Artichokes - Jerusalem', 'Displ transverse fx shaft of unsp rad, 7thR', 38.91, 32, '2018-07-30T00:14:56Z', '2019-04-24T04:17:58Z')
	,(9, 6, 'Venison - Striploin', 'Other biomechanical lesions of cervical region', 37.87, 12, '2019-01-19T11:55:32Z', '2018-07-26T07:30:46Z')
	,(10, 8, 'Snapple - Iced Tea Peach', 'Pain in unspecified knee', 80.01, 12, '2019-01-01T15:00:16Z', '2018-10-20T02:58:24Z')
	,(11, 1, 'Hersey Shakes', 'Toxic effect of 2-Propanol, accidental (unintentional), subs', 35.31, 1, '2018-12-20T17:29:17Z', '2019-02-25T06:19:19Z')
	,(12, 3, 'Oil - Margarine', 'Asphyxiation due to smothering in furniture, accidental', 74.77, 2, '2018-07-23T16:07:24Z', '2018-07-30T19:43:38Z')
	,(13, 8, 'Vermacelli - Sprinkles, Assorted', 'Oth complication of other internal prosth dev/grft, init', 11.78, 4, '2018-09-15T09:00:36Z', '2019-03-28T20:05:51Z')
	,(14, 3, 'Gelatine Leaves - Bulk', 'Disloc of distal interphaln joint of l mid finger, sequela', 17.73, 5, '2019-04-16T13:56:38Z', '2018-05-21T10:18:04Z')
	,(15, 8, 'Buffalo - Short Rib Fresh', 'Corrosion of larynx and trachea, subsequent encounter', 77.68, 6, '2018-11-23T02:47:00Z', '2018-12-09T06:25:04Z')
	,(16, 1, 'Nut - Pumpkin Seeds', 'Incarcerated fx of medial epicondyl of l humerus, sequela', 2.45, 8, '2018-12-19T08:54:34Z', '2018-09-22T09:22:45Z')
	,(17, 10, 'Coconut - Shredded, Sweet', 'Pedl cyc driver inj in clsn w statnry object in traf, init', 94.38, 12, '2018-09-13T14:24:46Z', '2018-07-31T13:45:11Z')
	,(18, 6, 'Zucchini - Yellow', 'Contact with other hot fluids, subsequent encounter', 92.05, 45, '2019-03-04T17:44:32Z', '2019-02-21T00:52:32Z')
	,(19, 2, 'Vodka - Lemon, Absolut', 'Abrasion of knee', 86.49, 22, '2018-07-14T15:05:55Z', '2018-12-20T22:40:29Z')
	,(20, 5, 'Monkfish Fresh - Skin Off', 'Driver of pk-up/van injured in nonclsn trnsp acc in traf', 98.11, 11, '2018-08-09T01:59:35Z', '2018-09-30T23:50:55Z')
	,(21, 7, 'Cheese - Goat', 'Open bite of abd wall, left lower quadrant w penet perit cav', 73.76, 11, '2018-07-29T13:17:45Z', '2019-03-01T03:40:41Z')
	,(22, 4, 'Peach - Halves', 'Ped on rolr-skt injured in clsn w pedl cyc nontraf, sequela', 76.41, 21, '2018-07-13T19:23:05Z', '2019-05-09T08:33:31Z')
	,(23, 6, 'Lemonade - Pineapple Passion', 'Burn due to loc fire on board oth powered wtrcrft, sequela', 50.41, 2, '2018-07-02T04:36:45Z', '2019-03-05T18:02:49Z')
	,(24, 7, 'Swiss Chard - Red', 'Subluxation of tarsometatarsal joint of unsp foot, init', 55.24, 1, '2019-02-02T11:17:31Z', '2018-07-19T11:30:52Z')
	,(25, 10, 'Sobe - Orange Carrot', 'Proc/trtmt not crd out bec pt decision for oth reasons', 74.61, 2, '2018-08-25T21:09:54Z', '2018-09-25T22:11:46Z')
	,(26, 7, 'Pork - Inside', 'Unspecified open wound of larynx', 82.41, 3, '2018-06-30T00:41:49Z', '2019-05-10T12:35:00Z')
	,(27, 8, 'Rum - White, Gg White', 'Inj nerves at abdomen, low back and pelvis level, sequela', 1.76, 2, '2018-11-08T11:52:00Z', '2018-08-27T06:49:50Z')
	,(28, 8, 'Cloves - Whole', 'Other otosclerosis, right ear', 28.63, 11, '2018-06-20T11:58:06Z', '2019-05-08T13:29:48Z')
	,(29, 1, 'Cheese - Bakers Cream Cheese', 'Intvrt disc stenos of neural canal of abd and oth regions', 52.38, 2, '2018-10-30T00:58:47Z', '2019-03-08T08:34:33Z')
	,(30, 5, 'Pasta - Fusili, Dry', 'Other specified acquired deformities of right lower leg', 12.16, 32, '2018-11-10T23:10:16Z', '2019-03-03T22:25:08Z')
	,(31, 3, 'Assorted Desserts', 'Other specified joint disorders, left wrist', 19.22, 1, '2019-02-19T16:04:41Z', '2018-09-23T21:07:19Z')
	,(32, 8, 'Salmon Steak - Cohoe 6 Oz', 'Farm as the place of occurrence of the external cause', 55.96, 1, '2018-09-23T23:21:16Z', '2018-08-05T15:23:14Z')
	,(33, 1, 'Mangoes', 'Drown due to fall/jump fr oth burning unpowered watercraft', 93.32, 2, '2019-01-06T18:53:34Z', '2018-11-18T21:26:03Z')
	,(34, 7, 'Compound - Orange', 'Muscle weakness (generalized)', 50.53, 1, '2019-02-21T22:14:16Z', '2018-10-09T23:28:07Z')
	,(35, 7, 'Olives - Green, Pitted', 'Strain of musc/tend the rotator cuff of unsp shoulder, subs', 72.81, 1, '2019-02-22T13:24:11Z', '2019-02-15T11:07:54Z')
	,(36, 1, 'Chicken - Leg / Back Attach', 'Moderate laceration of unspecified part of pancreas', 66.22, 11, '2019-02-20T08:28:08Z', '2018-10-30T12:54:27Z')
	,(37, 6, 'Sauce - Oyster', 'Crushing injury of unspecified foot', 70.27, 2, '2018-07-25T20:14:40Z', '2018-08-02T15:13:30Z')
	,(38, 8, 'Wine - Chardonnay South', 'Unsp injury of unsp msl/tnd at ank/ft level, unsp foot, subs', 61.06, 1, '2018-07-05T14:29:49Z', '2019-01-04T08:58:03Z')
	,(39, 7, 'Table Cloth 62x120 White', 'Milt op involving explosion of aerial bomb, milt, subs', 75.31, 1, '2019-04-03T02:32:18Z', '2019-04-13T14:31:08Z')
	,(40, 8, 'Plate Foam Laminated 9in Blk', 'Burn of second degree of unsp hand, unsp site, sequela', 24.47, 2, '2019-02-28T19:26:51Z', '2018-08-27T00:04:41Z')
	,(41, 2, 'Ocean Spray - Ruby Red', 'Garage of non-institutional residence as place', 25.26, 2, '2019-03-13T01:52:35Z', '2018-07-19T21:20:31Z')
	,(42, 5, 'Brandy - Orange, Mc Guiness', 'Mechanical ptosis of unspecified eyelid', 27.99, 1, '2019-03-27T01:01:49Z', '2018-12-03T09:21:19Z')
	,(43, 6, 'Calypso - Strawberry Lemonade', 'Passenger on bus injured in clsn w nonmtr vehicle nontraf', 5.78, 1, '2019-01-25T02:35:38Z', '2019-02-11T16:15:05Z')
	,(44, 4, 'Tomatoes - Grape', 'Adverse effect of enzymes, subsequent encounter', 88.33, 3, '2018-07-20T19:00:45Z', '2019-03-28T18:18:02Z')
	,(45, 9, 'Glass - Wine, Plastic, Clear 5 Oz', 'Other sprain of hip', 96.81, 1, '2018-06-11T20:04:42Z', '2018-11-27T15:57:24Z')
	,(46, 4, 'Pear - Prickly', 'Crushing injury of right ring finger', 19.93, 1, '2018-06-14T15:51:40Z', '2018-07-05T12:58:15Z')
	,(47, 9, 'Ecolab - Power Fusion', 'Spondylopathy in diseases classified elsewhere, site unsp', 17.21, 2, '2018-10-18T17:12:48Z', '2018-05-27T06:04:07Z')
	,(48, 4, 'Curry Powder', 'Other ossification of muscle, unspecified shoulder', 13.9, 1, '2019-05-10T16:32:38Z', '2018-05-25T11:49:34Z')
	,(49, 3, 'Pasta - Angel Hair', 'Oth pulmonary comp of anesth during preg, second trimester', 53.87, 1, '2019-02-16T17:34:38Z', '2018-12-28T13:21:48Z')
	,(50, 6, 'Wine - Piper Heidsieck Brut', 'Ped on rolr-skt injured in collision w 2/3-whl mv nontraf', 30.27, 1, '2018-06-15T20:43:47Z', '2019-04-26T06:59:09Z')
	,(51, 3, 'Muffin - Mix - Creme Brule 15l', 'Person outsd 3-whl mv inj in clsn w 2/3-whl mv in traf, init', 5.02, 1, '2018-11-03T15:52:37Z', '2019-04-28T15:22:07Z')
	,(52, 6, 'Wine - Magnotta - Pinot Gris Sr', 'Central perforation of tympanic membrane', 59.94, 2, '2018-06-07T23:17:24Z', '2018-10-25T09:13:43Z')
	,(53, 1, 'General Purpose Trigger', 'Fall on board merchant ship, initial encounter', 98.96, 3, '2019-03-02T10:03:03Z', '2019-02-26T21:54:30Z')
	,(54, 5, 'Tea - Herbal Orange Spice', 'Unsp open wound of left eyelid and periocular area, sequela', 67.55, 4, '2019-02-23T23:21:00Z', '2019-01-19T22:00:47Z')
	,(55, 2, 'Containter - 3oz Microwave Rect.', 'Fracture of angle of mandible, unspecified side, 7thD', 23.16, 2, '2019-02-09T10:31:57Z', '2019-04-26T02:03:15Z')
	,(56, 7, 'Steampan - Lid For Half Size', 'Pathological fracture in oth disease, left ankle, init', 28.79, 5, '2018-11-11T20:34:57Z', '2019-01-15T17:13:57Z')
	,(57, 8, 'Anisette - Mcguiness', 'Legal intervnt involving oth means, suspect injured, sequela', 79.16, 2, '2019-04-14T18:32:08Z', '2018-10-19T18:54:01Z')
	,(58, 7, 'Bread Base - Gold Formel', 'Chronic pain due to trauma', 77.92, 1, '2018-11-25T17:17:21Z', '2019-05-10T00:39:11Z')
	,(59, 1, 'Gingerale - Diet - Schweppes', 'Unspecified dislocation of right shoulder joint, sequela', 37.05, 1, '2019-01-09T23:50:23Z', '2018-11-21T17:51:59Z')
	,(60, 1, 'Bread - Pullman, Sliced', 'Laceration w/o fb of left thumb w damage to nail, init', 72.6, 2, '2018-08-08T18:07:46Z', '2018-07-27T05:20:43Z')
	,(61, 6, 'Coffee - Colombian, Portioned', 'Unspecified superficial injury of right hand, sequela', 25.57, 3, '2018-06-11T11:52:30Z', '2018-06-11T13:24:26Z')
	,(62, 7, 'Cookie Chocolate Chip With', 'Secondary malignant neoplasm of unsp kidney and renal pelvis', 68.76, 4, '2018-12-14T00:11:51Z', '2019-01-30T16:29:57Z')
	,(63, 8, 'Wine - Alsace Gewurztraminer', 'Sltr-haris Type II physeal fx upper end of r fibula, init', 18.59, 5, '2019-01-09T17:32:21Z', '2019-01-27T18:38:31Z')
	,(64, 7, 'Beef - Prime Rib Aaa', 'Minor laceration of celiac artery, subsequent encounter', 89.94, 3, '2018-09-27T10:56:23Z', '2019-01-13T19:32:47Z')
	,(65, 2, 'Pork - Bacon, Sliced', 'Placentitis, third trimester, not applicable or unspecified', 67.93, 2, '2018-11-14T17:26:47Z', '2018-09-16T16:28:43Z')
	,(66, 2, 'Quiche Assorted', 'Staphylococcal arthritis, hand', 80.16, 2, '2019-01-26T07:41:56Z', '2018-06-18T14:29:14Z')
	,(67, 1, 'Pur Value', 'Encounter for aftercare following other organ transplant', 79.5, 2, '2018-10-01T20:29:38Z', '2018-11-25T18:08:03Z')
	,(68, 1, 'Creme De Cacao Mcguines', 'Urethral caruncle', 28.08, 2, '2018-06-29T00:22:51Z', '2018-08-11T22:43:46Z')) AS Source(Id, InventoryTypeId, Name, Description, Price, Stock, AddedAt, ModifiedAt)
   ON Target.Id = Source.Id 
   WHEN MATCHED then 
   UPDATE SET 
   Target.InventoryTypeId = Source.InventoryTypeId,
   Target.Name=Source.Name,
   Target.Description = Source.Description,
   Target.Price = Source.Price,
   Target.Stock = Source.Stock,
   Target.AddedAt = Source.AddedAt,
   Target.ModifiedAt = Source.ModifiedAt
   WHEN NOT MATCHED THEN 
   INSERT VALUES(Source.InventoryTypeId, Source.Name, Source.Description, Source.Price, Source.Stock, Source.AddedAt, Source.ModifiedAt); 

   PRINT 'Finished Populating Inventory Table'
END

GO
