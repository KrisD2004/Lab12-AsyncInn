# Lab2-AysncInn


![ADiagram](C:\Users\KDots\OneDrive\Documents\GitHub\Lab2-AysncInn\DiagramLab11.png)

Joint Entity Table with Payload
The Rooms_Amen table is the joint entity table with a payload. It represents a many-to-many relationship between Rooms and Amen. The primary key of this table is (id).
The Rooms_Amen table also has two foreign keys:
rooms_id references the primary key id in the Rooms table.
Amen_id references the primary key ID in the Amen table.

Pure Join Table:
The Hotel_Rooms table acts as a pure join table that establishes a many-to-many relationship between Hotel_Location and Rooms. The primary key of this table is a composite key consisting of (hotel_id, rooms_id).
The Hotel_Rooms table also has two foreign keys:
hotel_id references the primary key id in the Hotel_Location table.
rooms_id references the primary key id in the Rooms table.

Enum:
There is no explicit enum in the provided table definitions.

Navigation Properties:
In the ERD, the relationships are represented by lines connecting the tables. For example, the Rooms table has a relationship with the Rooms_Amen table through the id and rooms_id fields.

Relationships between Tables:
Hotel_Location and Hotel_Rooms have a one-to-many relationship, where one hotel location can have multiple hotel rooms (hotel_id in Hotel_Rooms references id in Hotel_Location).
Rooms and Rooms_Amen have a one-to-many relationship, where one room can have multiple amenities (rooms_id in Rooms_Amen references id in Rooms).
Amen and Rooms_Amen have a one-to-many relationship, where one amenity can be associated with multiple rooms (Amen_id in Rooms_Amen references ID in Amen).
