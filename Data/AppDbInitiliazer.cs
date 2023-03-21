using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MyLibrary.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MyLibrary.Data
{
    public class AppDbInitiliazer
    {
        public static void Seed(IApplicationBuilder applicationBuilder)
        {
            using (var serviceScope = applicationBuilder.ApplicationServices.CreateScope())
            {
                var context = serviceScope.ServiceProvider.GetService<AppDbContext>();

                context.Database.EnsureCreated();

                //Inserting default Authors into the DB
                if (!context.Authors.Any())
                {
                    context.Authors.AddRange(new List<Author>()
                    {
                        new Author()
                        {
                            FullName = "Lucy Score",
                            Bio = "Lucy is a Wall Street Journal and #1 Amazon Kindle Store bestseller of romantic comedies and contemporary romance. ",
                            ProfilePictureURL = "https://images.gr-assets.com/authors/1571325113p8/8103667.jpg",
                        },
                        new Author()
                        {
                            FullName = "Erica Berry",
                            Bio = "Erica is a New York Journal bestseller of historic novels",
                            ProfilePictureURL = "https://images.gr-assets.com/authors/1664916081p8/22924113.jpg",
                        },
                        new Author()
                        {
                            FullName = "Roshani Chokshi",
                            Bio = "Roshani Chokshi is the award-winning author of the New York Times bestselling series The Star-Touched Queen, The Gilded Wolves and Aru Shah and The End of Time, which Time Magazine named one of the Top 100 Fantasy Books of All Time.",
                            ProfilePictureURL = "https://images.gr-assets.com/authors/1544538355p5/13695109.jpg",
                        },
                        new Author()
                        {
                            FullName = "Trang Thanh Tran",
                            Bio = "Trang Thanh Tran writes speculative stories with big emotions about food, belonging and the Vietnamese diaspora.",
                            ProfilePictureURL = "https://images.gr-assets.com/authors/1634561659p5/21706229.jpg",
                        }
                    });
                    context.SaveChanges();
                }
                //Inserting default Books into the DB
                if (!context.Books.Any())
                {
                    context.Books.AddRange(new List<Book>()
                    {
                        new Book()
                        {
                            Name = "Things We Hide from the Light",
                            Description = "Nash Morgan was always known as the good Morgan brother, with a smile and a wink for everyone. But now, this chief of police is recovering from being shot and his Southern charm has been overshadowed by panic attacks and nightmares.",
                            Price = 39.50,
                            ImageURL = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1673217326i/62022434.jpg",
                            ReleaseDate = 2023,
                            AuthorId = 1,
                            Genre = Genre.Romance
                        },
                        new Book()
                        {
                            Name = "Forever Never",
                            Description = "You don’t fall for your brother’s high school sweetheart, your boss’s daughter, or your ex-wife’s best friend. Especially when they’re all the same woman.",
                            Price = 29.50,
                            ImageURL = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1613666898i/57118988.jpg",
                            ReleaseDate = 2021,
                            AuthorId = 1,
                            Genre = Genre.Contemporary
                        },
                        new Book()
                        {
                            Name = "A Mystery of Mysteries: The Death and Life of Edgar Allan Poe",
                            Description = "A Mystery of Mysteries is a brilliant biography of Edgar Allan Poe that examines the renowned author’s life through the prism of his mysterious death and its many possible causes.",
                            Price = 14.99,
                            ImageURL = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1650844397i/60784591.jpg",
                            ReleaseDate = 2023,
                            AuthorId = 2,
                            Genre = Genre.Memoir
                        },
                        new Book()
                        {
                            Name = "The Last Tale of the Flower Bride",
                            Description = "A sumptuous, gothic-infused story about a marriage that is unraveled by dark secrets, a friendship cursed to end in tragedy, and the danger of believing in fairy tales—the breathtaking adult debut from New York Times bestselling author Roshani Chokshi.",
                            Price = 19.90,
                            ImageURL = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1675641961i/61150759.jpg",
                            ReleaseDate = 2023,
                            AuthorId = 3,
                            Genre = Genre.Fiction
                        },
                        new Book()
                        {
                            Name = "She Is a Haunting",
                            Description = "A house with a terrifying appetite haunts a broken family in this atmospheric horror, perfect for fans of Mexican Gothic.",
                            Price = 8.11,
                            ImageURL = "https://images-na.ssl-images-amazon.com/images/S/compressed.photo.goodreads.com/books/1664373083i/60784841.jpg",
                            ReleaseDate = 2023,
                            AuthorId = 4,
                            Genre = Genre.Horror
                        }
                    });
                    context.SaveChanges();
                }
            }
        }

    }
}
