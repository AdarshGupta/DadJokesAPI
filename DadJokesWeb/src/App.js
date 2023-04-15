import React from 'react';
import './App.css';

class DadJokesSearchForm extends React.Component{
  constructor(props){
    super(props);
    this.state = {
      searchQuery: '',
      jokes: []
    };
    this.handleChange = this.handleChange.bind(this);
    this.fetchAJoke = this.fetchAJoke.bind(this);
    this.searchJokes = this.searchJokes.bind(this);
    this.DAD_JOKES_SERVICE_API_ENDPOINT = 'https://localhost:7133/DadJokes';
  }

  handleChange(event){
    this.setState({
      searchQuery: event.target.value,
      jokes: []
    });
  }

  fetchAJoke(event){
    console.log("fetch a joke button clicked!");
    this.setState({
      searchQuery: ''
    });

    fetch(this.DAD_JOKES_SERVICE_API_ENDPOINT)
      .then((response) => response.json())
      .then((data) => this.setState({
        jokes: data
      }))
      .catch((error) => console.log("Error caused while fetching a joke. Error: " + error));
    
      event.preventDefault();
  }

  searchJokes(event){
    console.log("Search Jokes button clicked!");
    let queryWord = this.state.searchQuery;
    
    fetch(`${this.DAD_JOKES_SERVICE_API_ENDPOINT}?queryWord=${encodeURIComponent(queryWord)}`)
      .then((response) => response.json())
      .then((data) => this.setState({
        jokes: data
      }))
      .catch((error) => console.log("Error caused while fetching a joke. Error: " + error));
    
      event.preventDefault();
  }

  gethighlightedText(text, highlight){

    // Split on highlight term and add mark terms to those that match the query term (ignores case)
    const parts = text.split(new RegExp(`(${highlight})`, 'gi'));
    return (
      <span> 
        { 
          parts.map((part, i) => 
          (part.toLowerCase() === highlight.toLowerCase() ?
            <mark key={i}>{part}</mark> :
            part
          ))
        } 
      </span>
    );
  }

  getJokesSection(jokes, highlight){
    return jokes.map((joke) => 
        <div key={joke["id"]} className="joke-card">
          {this.gethighlightedText(joke["joke"], highlight)}
        </div>
    );
  }

  render(){
    const jokes = this.state.jokes;
    let queryWord = this.state.searchQuery; 
    let jokesSection;
    
    if(jokes.length === 0){
      jokesSection = <div></div>;
    }
    else if(jokes.length === 1 && !queryWord){
      jokesSection = <div className="joke-card">{jokes[0]["joke"]}</div>
    }
    else{
      let smallJokes = jokes.filter((joke) => joke["joke"].split(" ").length < 10);
      console.log("Small jokes length: " + smallJokes.length);

      let mediumJokes = jokes.filter((joke) => {
        const words = joke["joke"].split(" ");
        return words.length >= 10 && words.length < 20;
      });
      console.log("Medium jokes length: " + mediumJokes.length);

      let longJokes = jokes.filter((joke) => joke["joke"].split(" ").length >= 20);
      console.log("Long jokes length: " + longJokes.length);

      jokesSection = (
      <div>
        <h2>Small Jokes</h2>
        <div className="jokes-section">{smallJokes.length == 0 ? "No small jokes available." : this.getJokesSection(smallJokes, queryWord)}</div>

        <h2>Medium Jokes</h2>
        <div className="jokes-section">{mediumJokes.length == 0 ? "No medium jokes available." : this.getJokesSection(mediumJokes, queryWord)}</div>

        <h2>Long Jokes</h2>
        <div className="jokes-section">{longJokes.length == 0 ? "No long jokes available." : this.getJokesSection(longJokes, queryWord)}</div>
      </div>
      );
    }

    return (
      <div className="jokes-grid">
        <h1>Dad Jokes</h1>

        <div className="search-form">
          
          <input className="search-term" type="text" value={this.state.searchQuery} onChange={this.handleChange} />
          
          <div className="action-buttons">
            <button onClick={this.fetchAJoke}>Fetch a Joke</button>
            <button onClick={this.searchJokes}>Search Jokes</button>
          </div>

        </div>

        <div className="joke-results">
          {jokesSection}
        </div>
      </div>
    );
  }
}

function App() {
  return (
    <div className="App">
      <DadJokesSearchForm />
    </div>
  );
}

export default App;
