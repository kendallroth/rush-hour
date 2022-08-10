const fs = require("fs");

console.log("Parsing data");

const levelData = fs.readFileSync("./rush.txt", "utf8");
const levelLines = levelData.split("\n").filter((l) => l);

const levelsWithoutWalls = levelLines.filter((l) => !l.includes("x"))

console.log(`Levels (all): ${levelLines.length}`);
console.log(`Levels (no walls): ${levelsWithoutWalls.length}`);

fs.writeFileSync("./levels.txt", levelsWithoutWalls.join("\n"));