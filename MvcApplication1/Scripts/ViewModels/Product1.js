﻿function ProductViewModel() {
    //Make the self as 'this' reference
    var self = this;
    //Declare observable which will be bind with UI
    self.Id = ko.observable("");
    self.Name = ko.observable("");
    self.Price = ko.observable("");
    self.Category = ko.observable("");
    self.Quantity = ko.observable("");

    var Product = {
        Id: self.Id,
        Name: self.Name,
        Price: self.Price,
        Category: self.Category,
        Quantity: self.Quantity
    };

    self.Product = ko.observable();
    self.Products = ko.observableArray(); // Contains the list of products

    // Initialize the view-model
    $.ajax({
        url: 'Product/GetAllProducts',
        cache: false,
        type: 'GET',
        contentType: 'application/json; charset=utf-8',
        data: {},
        success: function (data) {
            self.Products(data); //Put the response in ObservableArray
        }
    });

    //// Calculate Total of Price After Initialization
    //self.Total = ko.computed(function () {
    //    var sum = 0;
    //    var arr = self.Products();
    //    for (var i = 0; i < arr.length; i++) {
    //        sum += arr[i].Price;
    //    }
    //    return sum;
    //});

    //Add New Item
    this.create = function () {
        if (Product.Name() != "" &&
        Product.Price() != "" && Product.Category() != "") {
            $.ajax({
                url: 'Product/AddProduct',
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: ko.toJSON(Product),
                success: function (data) {
                    self.Products.push(data);
                    self.Name("");
                    self.Price("");
                    self.Category("");
                    self.Quantity("");
                }
            }).fail(
            function (xhr, textStatus, err) {
                alert(err);
            });
        }
        else {
            alert('Please Enter All the Values !!');
        }
    }

    // Reset product details
    self.reset = function () {
        self.Name("");
        self.Price("");
        self.Category("");
        self.Quantity("");
    }

    // Delete product details
    self.delete = function (Product) {
        if (confirm('Are you sure to Delete "' + Product.Name + '" product ??')) {
            var id = Product.Id;

            $.ajax({
                url: 'Product/DeleteProduct/' + id,
                cache: false,
                type: 'POST',
                contentType: 'application/json; charset=utf-8',
                data: id,
                success: function (data) {
                    self.Products.remove(Product);
                }
            }).fail(
            function (xhr, textStatus, err) {
                self.status(err);
            });
        }
    }

    // Edit product details
    self.edit = function (Product) {
        self.Product(Product);
    }

    // Update product details
    self.update = function () {
        var Product = self.Product();

        $.ajax({
            url: 'Product/EditProduct',
            cache: false,
            type: 'PUT',
            contentType: 'application/json; charset=utf-8',
            data: ko.toJSON(Product),
            success: function (data) {
                self.Products.removeAll();
                self.Products(data); //Put the response in ObservableArray
                self.Product(null);
                alert("Record Updated Successfully");
            }
        })
        .fail(
        function (xhr, textStatus, err) {
            alert(err);
        });
    }

    // Cancel product details
    self.cancel = function () {
        self.Product(null);
    }
}

var viewModel = new ProductViewModel();
ko.applyBindings(viewModel);

function formatCurrency(value) {
    return "Rs. " + value.toFixed(2);
}
