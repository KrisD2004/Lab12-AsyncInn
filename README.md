# Lab2-AysncInn


## Lab 11
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

## Lab 14 Async Hotel Manage 

## AsyncInn Application Overview
The AsyncInn application is designed to manage hotel rooms and amenities for different hotels. It provides API endpoints that allow clients to perform various operations related to hotel rooms and amenities, such as retrieving room information, adding amenities to rooms, updating room details, and more.

## Key Components
Controllers: The application has two main controllers, HotelRoomsController and RoomsController, each responsible for handling different aspects of the API.

## Models:

HotelRoom: Represents a hotel room with properties such as ID, RoomID (room number), HotelID, Name, Price, and navigation properties for associated Hotel and Room objects.
Room: Represents a hotel room with properties such as ID, Name, Layout, and navigation properties for associated HotelRooms and RoomAmenities.
Context: The AsyncInnContext is used for database interactions, including managing hotel rooms, amenities, and their associations.

Routes and Operations

## HotelRoomsController:

* GET /api/HotelRooms: Retrieves a list of all hotel rooms.
* GET /api/Hotels/{hotelId}/Rooms: Retrieves all rooms for a specific hotel.
* GET /api/Hotels/{hotelId}/Rooms/{roomID}: Retrieves details of a specific room in a hotel.
* PUT /api/Hotels/{hotelId}/Rooms/{roomID}: Updates details of a specific room in a hotel.
* POST /api/HotelRooms: Creates a new hotel room.
* DELETE /api/Hotels/{hotelId}/Rooms/{roomID}: Deletes a specific room from a hotel.
 

## RoomsController:

* GET /api/Rooms: Retrieves a list of all rooms with associated data (hotel rooms and amenities).
* GET /api/Rooms/{id}: Retrieves details of a specific room.
* PUT /api/Rooms/{id}: Updates details of a specific room.
* POST /api/Rooms: Creates a new room.
* POST /api/Rooms/{roomId}/Amenity/{amenityId}: Adds an amenity to a specific room.
* DELETE /api/Rooms/{id}: Deletes a specific room.
* DELETE /api/Rooms/{roomId}/Amenity/{amenityId}: Removes an amenity from a specific room.

## Example Data
The README provided earlier includes examples of the data objects returned by each route, showcasing the structure and format of the data.

## 
This application can be used by clients (such as front-end applications or other APIs) to manage hotel room and amenity information. Clients can perform CRUD (Create, Read, Update, Delete) operations on hotel rooms and amenities through the provided API endpoints.

It's important to note that the application's functionality may evolve based on your specific business requirements and further development.

Remember that the provided overview is based on the code snippets and explanations you provided. If there are additional components or functionality not covered, please provide more details, and I'd be happy to help you further.

