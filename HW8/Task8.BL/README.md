### Task 7: The task is the extension of Task 6

#### To do:

1. **Add class `PaperBook`** to store:
   - The title of the book (a string, not empty, not null).
   - The publication date of the book (possibly null).
   - A list of `Isbns`.
   - The publisher.
   - A collection of `Authors` (first name, last name, date of birth (possibly null), limitation for names - not more than 200 symbols; the collection can be empty).

2. **Create a class to represent `EBook`** (electronic version of a book):
   - Store:
     - The title of the book (a string, not empty, not null).
     - Identifier of the internet resource where it is uploaded.
     - A list of available electronic formats.
     - A collection of `Authors` (first name, last name, date of birth (possibly null), limitation for names - not more than 200 symbols; the collection can be empty).

3. **Create class `Library`** which contains:
   - `Catalog` of books (all books are `PaperBook` or all books are `EBook`).
   - `PressReleaseItems`:
     - List of strings for `PaperBooks` catalog (list of all publishers in a `Library`).
     - For `EBook` catalog - list of all available electronic formats in a `Library`.
   - Use one of the `Isbns` as a key for the dictionary in the catalog for paper books.
   - Use an identifier for electronic books.
   - **Note:** Refactor `Catalog` class to:
     - Not check key (isbn) validity.
     - Or, make an outer key validator (preferred).

4. **Initialize two `Library` objects**:
   - One for `PaperBooks` with a list of publishers as `PressReleaseItems`.
   - One for `EBooks` with a list of formats.
   - Use the enclosed text file `books_info.csv` (can be opened by an Excel editor).

5. **Save books to XML and JSON repositories**:
   - Avoid including `PaperBook` and `EBook` specific information.

---

**Notes:**

- **Do not create classes like `ELibrary` or `PaperLibrary`!** Use one class `Library` for both paper and e-book catalogs.
- **It is recommended to use the Abstract Factory pattern** to create Libraries.