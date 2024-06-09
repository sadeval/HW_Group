using System;
using System.Collections.Generic;

namespace StudentManagement
{
    class Student
    {
        // Поля для хранения данных
        private string? name;
        private string? patronymic;
        private string? surname;
        private DateTime birthDate;
        private string? address;
        private string? phone;

        // Поля для хранения оценок
        private LinkedList<int> marks = new LinkedList<int>(); // Зачёты
        private LinkedList<int> courseworks = new LinkedList<int>(); // Курсовые работы
        private LinkedList<int> exams = new LinkedList<int>(); // Экзамены
        private double rating; // Рейтинг

        // Конструктор без параметров
        public Student() : this("Unknown", "Unknown", "Unknown", DateTime.MinValue, "Unknown", "Unknown")
        {
            Console.WriteLine("Constructor without parameters");
        }

        // Конструктор с параметрами: имя, отчество, фамилия 
        public Student(string name, string patronymic, string surname) : this(name, patronymic, surname, DateTime.MinValue, "Unknown", "Unknown")
        {
            Console.WriteLine("Constructor with name, patronymic, surname,");
        }

        // Конструктор с параметрами: имя, отчество, фамилия, адрес
        public Student(string name, string patronymic, string surname, string address) : this(name, patronymic, surname, DateTime.MinValue, address, "Unknown")
        {
            Console.WriteLine("Constructor with name, surname, patronymic, address");
        }

        // Основной конструктор с параметрами: имя, отчество, фамилия, дата рождения, адрес, телефон
        public Student(string name, string patronymic, string surname, DateTime birthDate, string address, string phone)
        {
            SetName(name);
            SetPatronymic(patronymic);
            SetSurname(surname);
            SetBirthDate(birthDate);
            SetAddress(address);
            SetPhone(phone);
            Console.WriteLine("Main constructor with all parameters");
        }

        // Методы для установки полей
        public void SetName(string name)
        {
            this.name = name;
        }
        public void SetPatronymic(string patronymic)
        {
            this.patronymic = patronymic;
        }

        public void SetSurname(string surname)
        {
            this.surname = surname;
        }

        public void SetBirthDate(DateTime birthDate)
        {
            this.birthDate = birthDate;
        }

        public void SetAddress(string address)
        {
            this.address = address;
        }

        public void SetPhone(string phone)
        {
            this.phone = phone;
        }

        // Методы для добавления оценок в зачёты, курсовые работы и экзамены
        public void AddMark(int value)
        {
            if (value < 1 || value > 12) return;
            marks.AddLast(value);
            ResetRating();
        }

        public void AddCoursework(int value)
        {
            if (value < 1 || value > 12) return;
            courseworks.AddLast(value);
            ResetRating();
        }

        public void AddExam(int value)
        {
            if (value < 1 || value > 12) return;
            exams.AddLast(value);
            ResetRating();
        }

        // Метод для редактирования оценок
        public void EditMarks(List<int> newMarks, List<int> newCourseworks, List<int> newExams)
        {
            marks = new LinkedList<int>(newMarks);
            courseworks = new LinkedList<int>(newCourseworks);
            exams = new LinkedList<int>(newExams);
            ResetRating();
        }

        // Показ всех данных о студенте
        public void PrintStudent()
        {
            Console.WriteLine($"Имя: {name}");
            Console.WriteLine($"Фамилия: {surname}");
            Console.WriteLine($"Отчество: {patronymic}");
            Console.WriteLine($"Дата рождения: {birthDate.ToShortDateString()}");
            Console.WriteLine($"Адрес: {address}");
            Console.WriteLine($"Номер телефона: {phone}");
            Console.Write("Оценки по зачётам: ");
            foreach (var mark in marks)
            {
                Console.Write($"{mark} ");
            }
            Console.WriteLine();
            Console.Write("Оценки по курсовым: ");
            foreach (var coursework in courseworks)
            {
                Console.Write($"{coursework} ");
            }
            Console.WriteLine();
            Console.Write("Оценки по экзаменам: ");
            foreach (var exam in exams)
            {
                Console.Write($"{exam} ");
            }
            Console.WriteLine();
            Console.WriteLine($"Рейтинг оценок: {rating:F1}");
        }

        // Метод для пересчета рейтинга
        private void ResetRating()
        {
            int totalGradesCount = marks.Count + courseworks.Count + exams.Count;

            if (totalGradesCount == 0)
            {
                rating = 0;
                return;
            }

            int totalSum = CalculateSum(marks) + CalculateSum(courseworks) + CalculateSum(exams);
            rating = (double)totalSum / totalGradesCount;
        }

        // Метод для вычисления суммы значений
        private int CalculateSum(LinkedList<int> list)
        {
            int sum = 0;
            foreach (var item in list)
            {
                sum += item;
            }
            return sum;
        }

        // Метод для получения рейтинга студента
        public double GetRating()
        {
            return rating;
        }

        // Методы для получения полей (для доступа из класса Group)
        public string? GetName()
        {
            return name;
        }

        public string? GetSurname()
        {
            return surname;
        }
    }

    class Group
    {
        private List<Student> students; // Список студентов
        private string? groupName; // Название группы
        private string? specialization; // Специализация группы
        private int courseNumber; // Номер курса

        // Конструктор по умолчанию (пустая группа без студентов)
        public Group()
        {
            students = new List<Student>();
            SetGroupName("Unknown");
            SetSpecialization("Unknown");
            SetCourseNumber(0);
        }

        // Конструктор с параметрами
        public Group(string groupName, string specialization, int courseNumber)
        {
            students = new List<Student>();
            SetGroupName(groupName);
            SetSpecialization(specialization);
            SetCourseNumber(courseNumber);
        }

        // Методы для установки полей
        public void SetGroupName(string groupName)
        {
            this.groupName = groupName;
        }

        public void SetSpecialization(string specialization)
        {
            this.specialization = specialization;
        }

        public void SetCourseNumber(int courseNumber)
        {
            this.courseNumber = courseNumber;
        }

        // Методы для получения полей
        public string? GetGroupName()
        {
            return groupName;
        }

        public string? GetSpecialization()
        {
            return specialization;
        }

        public int GetCourseNumber()
        {
            return courseNumber;
        }

        // Метод для добавления студента в группу
        public void AddStudent(Student student)
        {
            students.Add(student);
        }

        // Метод для редактирования данных о группе
        public void EditGroup(string groupName, string specialization, int courseNumber)
        {
            SetGroupName(groupName);
            SetSpecialization(specialization);
            SetCourseNumber(courseNumber);
        }

        // Метод для перевода студента из одной группы в другую
        public void TransferStudent(Group anotherGroup, Student student)
        {
            if (students.Remove(student))
            {
                anotherGroup.AddStudent(student);
            }
            else
            {
                throw new Exception("Такого студента в группе не существует.");
            }
        }

        // Метод для отчисления самого неуспевающего студента
        public void ExcludeWorstStudent()
        {
            if (students.Count == 0)
            {
                throw new Exception("Нет студента на отчисление.");
            }

            Student worstStudent = students[0];
            foreach (Student student in students)
            {
                if (student.GetRating() < worstStudent.GetRating())
                {
                    worstStudent = student;
                }
            }
            students.Remove(worstStudent);
        }

        // Метод для показа всех студентов группы
        public void PrintGroup()
        {
            Console.WriteLine($"Название группы: {groupName}");
            Console.WriteLine($"Специализация группы: {specialization}");
            Console.WriteLine($"Номер курса: {courseNumber}");
            Console.WriteLine("Студенты:");
            for (int i = 0; i < students.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {students[i].GetSurname()} {students[i].GetName()}");
            }
        }
    }

    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                // Создание группы 1 и студентов
                Group group1 = new Group("Группа 1", "Программирование", 1);
                Student student1 = new Student("Василий", "Алибабаевич", "Пупкин", new DateTime(1995, 02, 06), "ул. Литературная, д. 18", "+380955289873");
                Student student2 = new Student("Катя", "Ивановна", "Пушкарёва", new DateTime(1996, 05, 12), "ул. Пушкина, д. 20", "+380955289874");

                // Добавление студентов в группу
                group1.AddStudent(student1);
                group1.AddStudent(student2);

                // Показ группы
                group1.PrintGroup();
                Console.WriteLine();

                // Создание группы 2 и перевод студента
                Group group2 = new Group("Группа 2", "Дизайн", 2);
                group1.TransferStudent(group2, student1);

                // Показ групп после перевода
                Console.WriteLine("После перевода студента:");
                group1.PrintGroup();
                Console.WriteLine();
                group2.PrintGroup();
                Console.WriteLine();

                // Отчисление самого неуспевающего студента
                group1.ExcludeWorstStudent();
                Console.WriteLine("После отчисления самого неуспевающего студента:");
                group1.PrintGroup();
                Console.WriteLine();

                // Редактирование оценок студента
                List<int> newMarks = new List<int> { 12, 10, 11 };
                List<int> newCourseworks = new List<int> { 9, 9, 10 };
                List<int> newExams = new List<int> { 10, 8, 9 };
                student2.EditMarks(newMarks, newCourseworks, newExams);

                // Показ группы после редактирования оценок
                Console.WriteLine("После редактирования оценок студента:");
                group1.PrintGroup();
                Console.WriteLine();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Ошибка: {ex.Message}");
            }
        }
    }
}
