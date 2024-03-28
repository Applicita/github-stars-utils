# GitHub Starred Repositories Tools

This repository contains two tools:
1. **GitHub Starred Repositories Exporter**
2. **GitHub Starred Markdown Generator**
3. **GitHub Starred Mass Remove**

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
- The `GitHubToken` needs at least the `user_read` scope to read users starred repositories.
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

## 3. GitHub Starred Repositories Mass Unstarring Tool (GitHubStarredMassRemove)

### Overview
The GitHub Starred Repositories Mass Unstarring Tool is a console application designed to unstar repositories from your GitHub account in bulk. This tool is useful if you want to clean up your list of starred repositories efficiently.

### Features
- Unstars repositories listed in a CSV file from your GitHub account.
- Utilizes the GitHub API to perform bulk unstar operations.
- Provides console output on the progress and completion of the unstar process.

### Prerequisites
- .NET runtime environment.
- A GitHub Personal Access Token with appropriate permissions to unstar repositories.
- A CSV file containing a list of GitHub repositories you wish to unstar, typically generated by the GitHub Starred Repositories Exporter.

### Configuration
Before running the application, ensure you have a `GitHubToken` in your application's configuration file with at least `public_repo` scope for public repositories or `repo` scope for access to private repositories. This token is used for authenticating requests to the GitHub API.

### Usage
1. **Start the Application**: Run the console application. You can provide the path to the CSV file as a command-line argument or input it when prompted.

2. **Unstar Process**: The application connects to the GitHub API using the provided personal access token and begins unstarring repositories listed in the CSV file.

3. **Progress and Completion**: The application provides real-time console output of the unstar process. Upon completion, it displays a summary of the actions taken.

### Error Handling
If there are any issues (e.g., invalid token, network problems, or CSV file errors), the application will display an error message with details.

### Note
- The `GitHubToken` needs at least the `public_repo` scope to unstar public repositories. If you also wish to unstar private repositories, the `repo` scope is required.
- The CSV file format should match the one used by the GitHub Starred Repositories Exporter.

### Contributing
Contributions to enhance or fix issues in this tool are welcome. Please fork the repository, make your changes, and submit a pull request.

---

For more information or if you encounter issues, please open an issue in the GitHub repository of this project.

---

### License

MIT License

Copyright (c) 2024 Applicita Limited

Permission is hereby granted, free of charge, to any person obtaining a copy of this software and associated documentation files (the "Software"), to deal in the Software without restriction, including without limitation the rights to use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of the Software, and to permit persons to whom the Software is furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.