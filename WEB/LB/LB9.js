// Завдання 1 (Варіант 5)
// 1. Створити змінну "isLoggedIn" і призначити їй логічне значення у вигляді рядку, перевести це значення на логічне.
var isLoggedInStr = "true";
var isLoggedIn = (isLoggedInStr === "true"); // Перетворення в логічний тип boolean

// 2. Створити змінну під назвою "age" і призначити їй числове значення, перевести його у рядок.
var ageNum = 20;
var ageStr = ageNum.toString(); // Перетворення в рядок

// Виведення всіх змінних та їх типів у консоль
console.log("isLoggedInStr:", isLoggedInStr, "| Тип:", typeof isLoggedInStr);
console.log("isLoggedIn:", isLoggedIn, "| Тип:", typeof isLoggedIn);
console.log("ageNum:", ageNum, "| Тип:", typeof ageNum);
console.log("ageStr:", ageStr, "| Тип:", typeof ageStr);


