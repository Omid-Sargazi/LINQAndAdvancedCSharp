// See https://aka.ms/new-console-template for more information
using EfCoreBasics.Models;
using Microsoft.EntityFrameworkCore;

Console.WriteLine("Hello, World!");

using var db = new LibraryContext();

var author = new Author
{
    Name = "George Orwell",

};
db.Authors.Add(author);
db.Books.Add(new Book { Title = "1984", Author = author });
db.SaveChanges();

foreach(var b in db.Books.Include(b=>b.Author))
{
    Console.WriteLine($"{b.Title} by {b.Author.Name}");
}