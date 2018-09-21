# Hair Salon App

#### By Hyung Lee

## Description

A website that uses a database to keep track of all the stylists and clients to those stylists for a hair salon. The user will be able to add stylists and clients who see that stylist.

## How It Works

  * Homepage will have general functions such as adding new stylists, seeing the general details of stylists, editing and deleting specific stylists, and deleting all clients or stylists.
  >![Homepage Screenshot](/HairSalon/wwwroot/img/homepage.png)
  
  * From the homepage, the user can on the 'Details' button in the row of the stylist they want to see more specific details of that stylist. From the details page, the user can also edit the specific details of clients.
  >![Details Screenshot](/HairSalon/wwwroot/img/details.png)

## Setup/Installation Requirements

  1. Clone this repository.
  > $ git clone https://github.com/HyungNLee/HairSalon.Solution.git
  2. Use the Create the database needed for this application.
  > * CREATE DATABASE hyung_lee;
  > * USE hyung_lee;
  > * CREATE TABLE clients (id serial PRIMARY KEY, name VARCHAR(255), stylist_id INT);
  > * CREATE TABLE stylists (id serial PRIMARY KEY, name VARCHAR(255));
  3. Start your local server using MAMP or another similar program. Adjust user information and the ports in the DBConfiguration class in HairSalon/Startup.cs if needed.
  4. Navigate into the applicaton directory using a terminal program.
  > $ cd HairSalon.Solution/HairSalon
  5. Run the program with the following command.
  > dotnet run
  6. Once it has successfully start, go to the following URL in your browser. Adjust the localhost depending on the information shown to you in the terminal.
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