using System.Linq;
using System.Net;
using FluentAssertions;
using Xunit;

namespace ReactiveBits.FunctionalProgramming.AnonymousTypes
{
    public class AnonymousTypesExamples
    {
        [Fact]
        public void should_collect_authors_and_books()
        {
            var authors = new[]
            {
                new Author(1, "Mario Cioni"),
                new Author(2, "Giulio Andreotti")
            };

            var books = new[]
            {
                new Book(1, "La mamma", 1),
                new Book(2, "Berlinguer, ti voglio bene", 1),
                new Book(3, "Il Bacio", 2)
            };


            var items =
                from author in authors
                from book in books
                where author.Id == book.AuthorId
                select new {author = author.Name, title = book.Title};

            var result = from item in items
                select $"{item.author} wrote {item.title}";

            result.Should().Contain("Mario Cioni wrote La mamma");
            result.Should().Contain("Mario Cioni wrote Berlinguer, ti voglio bene");
            result.Should().Contain("Giulio Andreotti wrote Il Bacio");
        }

    }

    public class Book
    {
        public int Id { get; }
        public string Title { get; }
        public int AuthorId { get; }

        public Book(int id, string title, int authorId)
        {
            Id = id;
            Title = title;
            AuthorId = authorId;
        }
    }

    public class Author
    {
        public int Id { get; }
        public string Name { get; }

        public Author(int id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}