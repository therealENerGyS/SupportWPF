using Microsoft.EntityFrameworkCore;
using SupportWPF.Contexts;
using SupportWPF.Models;
using SupportWPF.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace SupportWPF.Services
{
    internal class OrderService
    {
        private static readonly DataContext _context = new();

        public static async Task SaveAsync(OrderRow orderRow)
        {
            var _orderRowEntity = new OrderRowEntity
            {
                Created = DateTime.Now,
                Subject = orderRow.Subject,
                Comment = orderRow.Comment,
                OrderStatus = "Not Started",
                Priority = orderRow.Priority,
                Deadline = DateTime.Now.AddDays(7),
            };

            var _productEntity = await _context.Products.FirstOrDefaultAsync(x => x.ProductName == orderRow.ProductName);
            if (_productEntity != null)
                _orderRowEntity.ProductId = _productEntity.ArticleNumber;
            else
                _orderRowEntity.Product = new ProductEntity
                {
                    ArticleNumber = Guid.NewGuid(),
                    ProductName = orderRow.ProductName,
                };

            var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == orderRow.FirstName && x.LastName == orderRow.LastName);
            if (_customerEntity != null)
                _orderRowEntity.CustomerId = _customerEntity.Id;
            else
                _orderRowEntity.Customer = new CustomerEntity
                {
                    Id = Guid.NewGuid(),
                    FirstName = orderRow.FirstName,
                    LastName = orderRow.LastName,
                    Email = orderRow.Email,
                    PhoneNumber = orderRow.PhoneNumber,
                };


            var _addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == orderRow.StreetName && x.StreetNumber == orderRow.StreetNumber && x.PostalCode == orderRow.PostalCode && x.City == orderRow.City);
            if (_addressEntity != null && _orderRowEntity.Customer != null)
                _orderRowEntity.Customer.AddressId = _addressEntity.Id;
            else
                if (_orderRowEntity.Customer != null)
                _orderRowEntity.Customer.Address = new AddressEntity
                {
                    StreetName = orderRow.StreetName,
                    StreetNumber = orderRow.StreetNumber,
                    PostalCode = orderRow.PostalCode,
                    City = orderRow.City
                };

            _context.Add(_orderRowEntity);
            await _context.SaveChangesAsync();
        }

        public static async Task<IEnumerable<OrderRow>> GetAllAsync()
        {
            var _orderRows = new List<OrderRow>();

            var _orderRowTest = _context.OrderRows.Include(x => x.Customer).ThenInclude(x => x!.Address).Include(x => x.Product).ToListAsync();

            foreach (var _orderRow in await _orderRowTest)
            {
                if (_orderRow != null)
                    if (_orderRow.Customer != null)
                        if (_orderRow.Product != null)
                            if (_orderRow.Customer.Address != null)
                                _orderRows.Add(new OrderRow
                                {
                                    Id = _orderRow.Id,
                                    Created = _orderRow.Created,
                                    Subject = _orderRow.Subject,
                                    ArticleNumber = _orderRow.ProductId,
                                    ProductName = _orderRow.Product.ProductName,
                                    FirstName = _orderRow.Customer.FirstName,
                                    LastName = _orderRow.Customer.LastName,
                                    Email = _orderRow.Customer.Email,
                                    PhoneNumber = _orderRow.Customer.PhoneNumber,
                                    StreetName = _orderRow.Customer.Address.StreetName,
                                    StreetNumber = _orderRow.Customer.Address.StreetNumber!,
                                    PostalCode = _orderRow.Customer.Address.PostalCode,
                                    City = _orderRow.Customer.Address.City,
                                    OrderStatus = _orderRow.OrderStatus,
                                    Comment = _orderRow.Comment,
                                    Priority = _orderRow.Priority,
                                    Deadline = _orderRow.Deadline
                                });
            }

            return _orderRows;
        }

        public static async Task UpdateAsync(OrderRow orderRow)
        {
            var _orderRowEntity = await _context.OrderRows.Include(x => x.Customer).ThenInclude(x => x!.Address).Include(x => x.Product).FirstOrDefaultAsync(x => x.Id == orderRow.Id);
            if (_orderRowEntity != null)
            {
                if(!string.IsNullOrEmpty(orderRow.Subject))
                    _orderRowEntity.Subject = orderRow.Subject;
                if(!string.IsNullOrEmpty(orderRow.Comment))
                    _orderRowEntity.Comment = orderRow.Comment;
                if (!string.IsNullOrEmpty(orderRow.OrderStatus))
                    _orderRowEntity.OrderStatus = orderRow.OrderStatus;
                if (!string.IsNullOrEmpty(orderRow.Priority))
                    _orderRowEntity.Priority = orderRow.Priority;

                if (!string.IsNullOrEmpty(orderRow.ProductName))
                {
                    var _productEntity = await _context.Products.FirstOrDefaultAsync(x => x.ArticleNumber == orderRow.ArticleNumber && x.ProductName == orderRow.ProductName);
                    if (_productEntity != null)
                        _orderRowEntity.ProductId = _productEntity.ArticleNumber;
                    else
                        _orderRowEntity.Product = new ProductEntity
                        {
                            ArticleNumber = orderRow.ArticleNumber,
                            ProductName = orderRow.ProductName
                        };
                }
                if (!string.IsNullOrEmpty(orderRow.FirstName) || !string.IsNullOrEmpty(orderRow.LastName) || !string.IsNullOrEmpty(orderRow.Email) || !string.IsNullOrEmpty(orderRow.PhoneNumber))
                {
                    var _customerEntity = await _context.Customers.FirstOrDefaultAsync(x => x.FirstName == orderRow.FirstName && x.LastName == orderRow.LastName && x.Email == orderRow.Email && x.PhoneNumber == orderRow.PhoneNumber);
                    if (_customerEntity != null)
                        _orderRowEntity.CustomerId = _customerEntity.Id;
                    else
                        _orderRowEntity.Customer = new CustomerEntity
                        {
                            FirstName = orderRow.FirstName,
                            LastName = orderRow.LastName,
                            Email = orderRow.Email,
                            PhoneNumber = orderRow.PhoneNumber,
                        };
                }

                if(!string.IsNullOrEmpty(orderRow.StreetName) || !string.IsNullOrEmpty(orderRow.StreetNumber) || !string.IsNullOrEmpty(orderRow.PostalCode) || !string.IsNullOrEmpty(orderRow.City))
                {
                    var _addressEntity = await _context.Addresses.FirstOrDefaultAsync(x => x.StreetName == orderRow.StreetName && x.StreetNumber == orderRow.StreetNumber && x.PostalCode == orderRow.PostalCode && x.City == orderRow.City);
                    if (_addressEntity != null)
                        _orderRowEntity.Customer.AddressId = _addressEntity.Id;
                    else
                        _orderRowEntity.Customer.Address = new AddressEntity
                        {
                            StreetName = orderRow.StreetName,
                            StreetNumber = orderRow.StreetNumber,
                            PostalCode = orderRow.PostalCode,
                            City = orderRow.City,
                        };
                }

                _context.Update(_orderRowEntity);
                await _context.SaveChangesAsync();
            }
        }
    }
}
