import React from 'react';
import './App.css';
import News from './News/News'
import NavBar from './NavBar/NavBar'

class App extends React.Component {
  constructor(props) {
    super(props);
    this.state = {            
      news: "пока ничего",     
    };
  }

  async componentDidMount() {
     var result=await fetch(`https://localhost:44376/api/News`)
     .then(response => response.json())
     this.setState({news:result})
} 
  render(){
  return (   
    <div>
    <NavBar />
    <News news={this.state.news}/>    
    </div> 
         );
}}

export default App;
