import React from 'react';


class News extends React.Component {

    constructor(props) {
        super(props);
        this.state = {
          news: "this.props.news"
        };
      }
      static getDerivedStateFromProps(props, state){
      return {news: props.news}
    }
    render() {
return (
    <div>
        {Object.keys(this.state.news).map(i=>{
        return <div>
            <h1>{this.state.news[i].heading}</h1>
            <br />
            <h>{this.state.news[i].content}</h>
            <br />
            <h>Date: {this.state.news[i].date}</h>
            <br />
            <h>PositiveIndex:  {this.state.news[i].positiveIndex}</h>
            <hr />
            <br />
            </div>
    })}
    </div>   
)
}}

export default News;