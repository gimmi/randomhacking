const iterations: number[] = [1, 2, 3, 4, 5];

console.dir(iterations);

iterations.forEach(function(iteration) {
    console.log(`Iteration ${iteration}...`);
});

process.exit(1);
