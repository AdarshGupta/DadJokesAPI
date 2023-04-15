# DadJokesAPI
There are two options the user can choose from:
1. Fetch a random joke.
2. Accept a search term and display the first 30 jokes containing that term. The matching term should be emphasized in some simple way (upper, quotes, angle brackets, etc.). The matching jokes should be grouped by length: Short (< 10 words), Medium (< 20 words), Long (>= 20 words)

# Setup
1. Run the .NET Core Web API application present at location "DadJokesAPI" folder in Visual Studio.
2. To run the frontend part of the application (which is a React app):
    1. `cd DadJokesWeb`
    2. `npm install`
    3. Change the backend API endpoint in `src/App.js` file at line 14 to your local running web API instance.
    4. `npm start`
