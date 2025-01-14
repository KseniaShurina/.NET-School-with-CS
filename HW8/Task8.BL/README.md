# Task 8: Extension of Task 7

## Objective
The goal of this task is to enhance the `EBook` class by adding a property for storing the number of pages in a book and implementing functionality to initialize this property.

## To-Do

1. **Add a Property to the `EBook` Class**
   - Add a property named `Pages` to the `EBook` class to store the number of pages in an electronic book.

2. **Fetch and Populate the `Pages` Property**
   - Use the `Identifier` field of the `EBook` to construct a URL to retrieve the book's page count.
   - The URL format will be: `https://archive.org/details/{identifier}`.
   - Download the HTML page from the constructed URL asynchronously.
   - Parse the HTML to find the number of pages using the following sample HTML element:
     ```html
     <span itemprop="numberOfPages">38</span>
     ```

3. **Initialize the `Pages` Property for All EBooks in a Library**
   - Iterate through all books in a `Library`.
   - Fetch the number of pages for each `EBook` and set the `Pages` property.