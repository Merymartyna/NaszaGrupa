import React, { Component } from 'react';

export class Home extends Component {
    static displayName = Home.name;

    constructor() {
        super();
        this.displayName = this.constructor.name;
    }
    constructor(props) {
        super(props);

        this.state = {
            items: [],
        }
    }
    getProducts() {
        fetch('https://localhost:44364/api/api')
            .then(res => res.json())
            .then(data => this.setState({ items: data }))
            .catch(err => console.log(err))
    }
    componentDidMount() {
        this.getProducts();
    }
    render() {
        const listItems = this.state.items.map((item, index) => (
            <div key={index.toString()}>
                {item.name}
            </div>
        ));

        return (
            <div>
                <div>
                    {listItems}
                </div>
            </div>
        );
    }
    render() {
        return (
            <div>
                {this.displayName}
            </div>
        );
    }
}