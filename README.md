# Search.Service

Welcome to the Search.Service Microservice repository! This repository contains the code for searching for blog posts using full-text search

## Table of Contents

- [Introduction](#introduction)
- [Features](#features)
- [Getting Started](#getting-started)
- [Usage](#usage)
- [API Documentation](#api-documentation)
- [Contributing](#contributing)
- [License](#license)

## Introduction

The Search.Service project is designed to provide functionality for managing blog posts using the CQRS (Command Query Responsibility Segregation) and Mediator design patterns. It includes features for creating, updating, retrieving, and deleting blog posts. The project is built using .Net CORE, leveraging the power of CQRS and Mediator for better separation of concerns and efficient handling of commands and queries.


## Features

Responsible for searching for blog posts using full-text search

## Getting Started

To get started with the Search.Service project, follow these steps:

1. [Installation instructions](#installation-instructions)
2. [Configuration setup](#configuration-setup)
3. [Running the project](#running-the-project)

### Installation Instructions

1. Clone the repository: `git clone https://github.com/hiralpatel-cloud-evangelist/Search.Service.git`
2. Navigate to the project directory: `cd Search.Service`
3. Install dependencies: `dotnet restore`

### Configuration Setup

To configure the Post.Service project:

1. Rename `appsettings.json.example` to `appsettings.json`.
2. Open `appsettings.json` and update the configuration settings.

### Running the Project

To run the Post.Service project:

1. Build the project: `dotnet build`
2. Run the application: `dotnet run`
3. Access the application in a web browser at `http://localhost:5000`

## Usage

Once the application is up and running, you can use the provided API endpoints to manage blog posts. Detailed API documentation can be found at [http://52.186.89.164/swagger/index.html](http://52.152.245.149/SWAGGER/index.html)

## Architecture

![image](https://github.com/hiralpatel-cloud-evangelist/Search.Service/assets/133631869/f50a7272-da2d-48f3-befc-028fef18769f)

## API Documentation

Detailed API documentation can be found at [http://52.186.89.164/swagger/index.html](http://52.152.245.149/SWAGGER/index.html)


## Contributing

Contributions to the Search.Service project are welcome! If you find any issues or have improvements to suggest, please follow the steps in [CONTRIBUTING.md](./CONTRIBUTING.md) to contribute.

