import React, { Component } from 'react';

export const Counter = () => {
	//static displayName = Counter.name;

	const [count, setCount] = useState < number > 0;

	incrementCounter = () => {
		setCount(count + 1);
	};
	return (
		<div>
			<h1>Counter</h1>

			<p>This is a simple example of a React component.</p>

			<p aria-live="polite">
				Current count: <strong>{this.state.currentCount}</strong>
			</p>

			<button className="btn btn-primary" onClick={this.incrementCounter}>
				Increment
			</button>
		</div>
	);
};
