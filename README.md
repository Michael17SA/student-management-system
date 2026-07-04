# Student Management System

A C# console application for managing student records, grades, averages, searches, removals, and student totals.

## Overview

This project demonstrates object-oriented programming in C# using abstraction, inheritance, polymorphism, collections, validation, exception handling, and a menu-driven console interface.

## Features

- Adds new student records
- Validates student IDs using the `S12345` format
- Stores three grades per student
- Calculates and displays grade averages
- Displays all student records
- Searches for a student by ID
- Removes a student by ID
- Tracks the total number of students
- Handles invalid user input with clear error messages

## Technologies Used

- C#
- .NET 9
- Console application
- Object-oriented programming

## How to Run

```bash
dotnet run
```

## Challenges and Limitations

- Designing the logic for adding students required careful thinking about validation and exception handling.
- Student ID validation was challenging because the ID needed a specific format: the letter `S` followed by five digits.
- Formatting output clearly required practice with interpolated strings.
- Applying object-oriented programming principles without overcomplicating the code was an important learning step.

## What I Learned

- Abstraction through a base `Person` class
- Inheritance through the `Student` class
- Polymorphism by overriding `DisplayInfo`
- List-based record storage
- Input validation and exception handling
- Menu-driven application design
