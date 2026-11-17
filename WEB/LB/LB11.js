// Завдання 1 (Варіант 5)
// Створіть функцію під назвою "calculateBMI", яка приймає вагу в кг і зріст у метрах та повертає ІМТ.
console.log("--- Завдання 1 ---");
function calculateBMI(weightKg, heightMeters) {
    var bmi = weightKg / (heightMeters * heightMeters);
    return bmi;
}

var myWeight = 75; // вага у кг
var myHeight = 1.80; // зріст у метрах
var resultBMI = calculateBMI(myWeight, myHeight);
console.log("Розрахований ІМТ (BMI):", resultBMI.toFixed(2));

// Завдання 2 (Варіант 5)
// Створіть об’єкт, що представляє фільм (назва, режисер, рік випуску). Додайте ще одну властивість і виведіть її.
console.log("\n--- Завдання 2 ---");
var movie = {
    title: "Інтерстеллар",
    director: "Крістофер Нолан",
    year: 2014
};

// Додавання нової властивості
movie.genre = "Наукова фантастика";

// Виведення повідомлення з деталями фільму та новою властивістю
console.log("Фільм: " + movie.title + ", Режисер: " + movie.director + ", Рік: " + movie.year);
console.log("Додана властивість (Жанр):", movie.genre);

// Завдання 3 (Варіант 5)
// Масив об’єктів (студенти з ім'ям та віком). Метод filter для студентів віком від 18 років.
console.log("\n--- Завдання 3 ---");
var students = [
    { name: "Олександр", age: 17 },
    { name: "Владислав", age: 19 },
    { name: "Марія", age: 18 },
    { name: "Анастасія", age: 16 },
    { name: "Дмитро", age: 21 }
];

var adultStudents = students.filter(function(student) {
    return student.age >= 18;
});

console.log("Студенти віком від 18 років:", adultStudents);


