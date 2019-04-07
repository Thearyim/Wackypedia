# Wackypedia

#### _03/14/2019_

## Author
 **Young Liu** youngliu92@gmail.com

**Theary Im**
thearyim@gmail.com

**Tristan Setha**
tristansetha@gmail.com

**Florin Mirica**
miricaflorin@hotmail.com

## Description

**_This wiki page allows the user to input articles. For each article, the author can add multiple sections to the article, and edit the articles and sections. Which users have contributed to which articles are tracked, and you can view all of the articles an author has contributed to._**

## Specs

1. User can view all articles

  - Example Input

    > View All articles

  - Example Output
    >  All Article
    - Article 1
    - Article 2
    - Article 3


2. User can add articles to the wiki page.

  - Example Input    

    > Add New Article

  - Example Output
    > New Article
    - title
    - section 1
    - section 2  


3. User can add multiple sections to each article.

    - Example Input    

      > Add a new section

    - Example Output

        > Article 1
        - subject
        - section 1
        - section 2
        - section 3

4. User can add imagelink to each article

    - Example Input

    > Add imagelink

    - Example Output

    > Article 1
      - subject
      - image
      - section 1
      - section 2

6. User can edit articles.

    - Example Input

    > Edit Articles 1

    - Example Output

    > New Article 1


7. User can edit each section in article.

    - Example Input

    > Article 1
      - Edit section 1

    - Example Output

    > Article
      - New section 1

8. User can search for article by title.

    - Example Input

    > Search for "habits"

    - Example Output

    > Results:
    > Articles found:
      - 30 One-Sentence Stories From People Who Have Built Better Habits
      - 10 Habits Successful People Do

9. User can see all the authors that have contributed to an article.

    - Example Input

    > Click on an article

    - Example Output

    > All authors are listed on the bottom of the page.

10. User can see all the articles a user has contributed to.

    - Example Input

    > Click on an author

    - Example Output

    > All articles the author has contributed to will appear on the page.


## Setup/Installation Requirements
**.NET Core is Required for this project to function.**

Download .NET Core 2.1.3 SDK and .NET Core Runtime 2.0.9 and install them. Download and install .NET Core 1.1:  
https://dotnet.microsoft.com/download/dotnet-core/1.1

Download and install Mono:  
https://www.mono-project.com/

1. Clone this repository:
    "$git clone https://github.com/Youngliu/Wackypedia.Solution"

2. Setup the Database. Import the Database using the SQL files and command shown below:

  * > CREATE DATABASE wackypedia;
  * > USE wackypedia;
  * > CREATE TABLE article (id serial PRIMARY KEY, title VARCHAR(255));
  * > CREATE TABLE author (id serial PRIMARY KEY, name VARCHAR(255));
  * > CREATE TABLE articles_authors (id serial PRIMARY KEY, article_id INT(11), author_id INT(11));

  * > CREATE DATABASE wackypedia_test;
  * > USE wackypedia_test;
  * > CREATE TABLE article (id serial PRIMARY KEY, title VARCHAR(255));
  * > CREATE TABLE author (id serial PRIMARY KEY, name VARCHAR(255));
  * > CREATE TABLE articles_authors (id serial PRIMARY KEY, article_id INT(11), author_id INT(11));

3. Change into the work directory: $ cd Wackypedia.Solution

4. To edit the project, open the project in your preferred text editor.

5.   To run the tests, use these commands:
    * > $ cd Wackypedia.Solution/Wackypedia.Tests
    * > $ dotnet test

6.  To run the program, first navigate to the location of the Wackypedia.cs file then compile and execute:
   * > $ cd Wackypedia.Solution/Wackypedia
   * > $ dotnet build
   * > $ dotnet run

7. Navigate to http://localhost:5000 in your browser to view the splashpage.


## Known Bugs

_No Known Bugs._

## Technologies Used
>
> - C#
> - HTML
> - CSS
> - .NET
> - Git
> - MAMP
> - MSTest


### License

*MIT License*

Copyright (c) 2019

**Young Liu**

**_Theary Im_**

**Tristan Setha**

**Florin Mirica**
