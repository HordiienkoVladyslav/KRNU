// Завдання 1 (Варіант 5)
// Створіть змінні "num1" і "num2" і використовуйте умовний оператор, щоб перевірити, чи num1 більше за num2.
console.log("--- Завдання 1 ---");
var num1 = 15;
var num2 = 10;

if (num1 > num2) {
    console.log("num1 більше за num2");
} else {
    console.log("num2 більше за num1");
}

// Завдання 2 (Варіант 5)
// Створіть змінну "weather" і використовуйте оператор вибору (switch), з гілкою default.
console.log("\n--- Завдання 2 ---");
var weather = "сонячно";

switch (weather) {
    case "сонячно":
        console.log("Сонячний день");
        break;
    case "дощ":
        console.log("Йде дощ");
        break;
    case "хмарно":
        console.log("Хмарний день");
        break;
    case "сніг":
        console.log("Іде сніг");
        break;
    default:
        console.log("Невідомі погодні умови");
}

// Завдання 3 (Варіант 5)
// Використайте цикл while, щоб створити зворотний відлік від 1 до 10, записуючи кожне число на консоль.
// (Примітка: відлік від 10 до 1 за логікою зворотного відліку в діапазоні 1..10)
console.log("\n--- Завдання 3 ---");
var count = 10;
while (count >= 1) {
    console.log("Відлік:", count);
    count--;
}


