const iterations: number[] = [1, 2, 3, 4, 5];

console.dir(iterations);

iterations.map(i => `Iteration ${i}...`).forEach(i => { console.log(i); });

process.exit(1);
