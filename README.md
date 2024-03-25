# GitHub Starred Repositories Tools

This repository contains two tools:
1. **GitHub Starred Repositories Exporter**
2. **GitHub Starred Markdown Generator**

## 1. GitHub Starred Repositories Exporter (GitHubStarredRepoExporter)

### Overview
GitHub Starred Repositories Exporter is a console application designed to fetch and export the list of repositories you have starred on GitHub into a CSV file. This tool is particularly useful for backing up your starred repositories or analyzing them offline.

### Features
- Fetches starred repositories from your GitHub account.
- Exports the list of repositories to a CSV file with detailed information about each repository.
- Organizes the exported data by the most recently pushed repositories.
- Handles pagination for GitHub's API to fetch all starred repositories.
- Generates CSV files with a unique timestamp in the filename for easy identification.

### Prerequisites
- .NET runtime environment.
- A GitHub Personal Access Token with appropriate permissions to access your starred repositories.

### Configuration
Before running the application, ensure you have a `GitHubToken` in your application's configuration file. This token is used to authenticate requests to the GitHub API.

### Usage
1. **Start the Application**: Run the console application. It will display a message indicating the start of the export process.

2. **Export Process**: The application will automatically connect to the GitHub API using the provided personal access token. It fetches the starred repositories and exports them to a CSV file.

3. **CSV File**: Once the export is complete, the application will notify you of the completion and the location of the generated CSV file. The file is named in the format `starred_repos_YYYYMMDD_HHMMSS.csv` and is located in a `GitHubStarredUtils` directory inside your 'My Documents' folder.

4. **Completion**: After the export process is complete, press any key to exit the application.

### Error Handling
In case of any errors (like missing GitHub token, API issues, etc.), the application will display an error message describing the issue.

### Note
- Ensure your GitHub Personal Access Token has the correct permissions to access your starred repositories.
- The application provides console output on the progress, including the number of pages fetched from the GitHub API and the rate limit status.

### Example Output

```plaintext
Id        | Node_Id                   | Name             | FullName | Login     | Id     | Node_Id                   | Avatar_Url                               | Html_Url                   | Html_Url                               | Description                                    | Url                                     | Language | Key | Name        | Url                                  | Spdx_Id | Node_Id      | Html_Url | Pushed_At        | Created_At       | Updated_At       | Topics                                   | Lists
----------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
348132133 | MDEwOlJlcG9zaXRvcnkzND... | fluentui-blazor  |          | microsoft | 6154722 | MDEyOk9yZ2FuaXphdGlvb... | https://avatars.githubusercontent.com... | https://github.com/micr... | https://github.com/microsoft/fluent... | Microsoft Fluent UI Blazo...                   | https://api.github.com/repos/microso... | C#       | mit | MIT License | https://api.github.com/licen...      | MIT     | MDc6TGljZ... |          | 03/25/2024 20:18 | 03/15/2021 21:48 | 03/25/2024 18:35 | adaptive-ui;blazor;compo...              | adaptive-ui;blazor;compo...

In this example it would put this GitHub repository in two markdown files, one for `adaptive-ui` and another for `blazor`.
```

### Contributing
Feel free to fork the repository, make changes, and submit pull requests for any improvements or bug fixes.

---

For more information or if you encounter issues, please open an issue in the GitHub repository of this project.

## 2. GitHub Starred Markdown Generator

### Overview
The GitHub Starred Markdown Generator is a console application designed to read a list of GitHub repositories from a CSV file and process them generating one markdown file per unique value in column with the header value of `lists`, each value can be seperated in the column by a semi-colon `;`. It is intended to complement the GitHub Starred Repositories Exporter by providing additional processing capabilities.

### Prerequisites
- .NET runtime environment.
- A CSV file containing a list of GitHub repositories, typically generated by the GitHub Starred Repositories Exporter.

### Usage
1. **Start the Application**: Run the console application. You have two options for specifying the CSV file:
   - Provide the file path as a command-line argument when running the application.
   - If no command-line argument is provided, the application will prompt you to enter the path to the CSV file.

2. **CSV Processing**: Once the file path is provided, the application will attempt to read and process the CSV file.

3. **Completion**: After the processing is complete (note: the actual processing logic is yet to be implemented), the application will display a completion message. Press any key to exit the application.

### Error Handling
If no file path is provided or if there is an issue with the CSV file, the application will display an error message and exit.

### Contribution
As with the GitHub Starred Repositories Exporter, feel free to contribute to the development and enhancement of this tool.

---

For more information or if you encounter issues, please open an issue in the GitHub repository of this project.

### License

MIT License

Copyright (c) 2024 Applicita Limited

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.