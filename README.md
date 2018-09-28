# Hair Salon App

#### By Hyung Lee

## Description

A website that uses a database to keep track of all the stylists and clients to those stylists for a hair salon. The user will be able to add stylists and clients who see that stylist.

## How It Works

  * Homepage will have three tabs: Stylists, Clients, Specialities. The user can click on these tabs to get a general overview of the data in each corresponding table in the MySql database.
  >![Homepage Screenshot](/HairSalon/wwwroot/img/homepage.png)
  
  * From the homepage, the user can select each tab to bring up different information. There will be buttons to edit information on specific data sets or the user can click on the name of a specific data set to go to the details page of that data set.
  >![Details Screenshot](/HairSalon/wwwroot/img/details.png)

  * Once the user clicks on a name and enters the page of details for a specific item they will be presented with more options. For example, once the user clicks on the name of a stylist they are presented with a list of clients that see that stylist along with a list of specialities that stylist is know for.

  * From here they can enter new clients, delete or edit existing clients, add specialities or delete specialities.

## Setup/Installation Requirements

  1. Clone this repository.
  > $ git clone https://github.com/HyungNLee/HairSalon.Solution-Update.git
  2. Use the Create the database needed for this application.
  > * CREATE DATABASE hyung_lee;
  > * USE hyung_lee;
  > * CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id INT);
  > * CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
  > * CREATE TABLE specialities (id serial PRIMARY KEY, name VARCHAR(255));
  > * CREATE TABLE stylists_specialities (id serial PRIMARY KEY, stylist_id INT, speciality_id INT);
  3. Start your local server using MAMP or another similar program. Adjust user information and the ports in the DBConfiguration class in HairSalon/Startup.cs if needed.
  4. Navigate into the applicaton directory using a terminal program.
  > $ cd HairSalon.Solution-Update/HairSalon
  5. Run the following command to restore the HairSalon.csproj file.
  > $ dotnet restore
  6. Run the program with the following command.
  > $ dotnet run
  7. Once it has successfully start, go to the following URL in your browser. Adjust the localhost depending on the information shown to you in the terminal.
  > http://localhost:5000/
  
## Known Bugs

None known in this version.

## Support and contact details

  - Hyung Lee - github.com/HyungNLee

## Technologies Used

  - VS Code
  - C#/.Net Core 1.1
  - MySql
  - MAMP
  - Git
  - GitHub
  - CSS
  - HTML
  
This software is licensed under the MIT license.

Copyright (c) 2018 **Hyung Lee**