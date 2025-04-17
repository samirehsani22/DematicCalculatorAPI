# 🧮 Dematic Calculator API

This project was developed as part of a **technical assessment for the job interview**. It demonstrates a clean, modular, and extensible architecture to perform arithmetic operations via a RESTful API using **ASP.NET Core**.

The API supports both **JSON** and **XML** formats, allows **nested operations**, and follows **Clean Architecture** principles to separate responsibilities and make the system easy to extend and maintain.

---

## 🚀 Features

- Accepts input via HTTP POST in **JSON** or **XML** format
- Supports arithmetic operations:
  - Addition
  - Multiplication
- Handles **nested operations** recursively (e.g., `(2 + 3) + (4 * 5)`)
- Uses a factory pattern to create operations dynamically
- Clean separation of concerns using **SOLID** principles
- Written with testability in mind – includes **unit tests** using `xUnit` and `Moq`
- Automatically run build and test on every **PUSH** to github main branch
---

## 🧱 Architecture Overview

The project follows **Clean Architecture**, with layers divided into separate projects for better maintainability and testability.

