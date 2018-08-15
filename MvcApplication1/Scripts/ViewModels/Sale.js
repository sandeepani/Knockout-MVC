

    var Product = function(){
        var self = this;
        self.Id = ko.observable("");
        self.Name = ko.observable("");
        self.Price = ko.observable(0);
        self.Category = ko.observable("");
    };

    var SaleDetail = function () {
        var self = this;
        self.Id = ko.observable("");
        self.Product = ko.observable();
        self.ProductId = ko.pureComputed(function () {
            return self.Product() ? self.Product().Id : 0;
        }); 
        self.Products = ko.observableArray();
        self.Quantity = ko.observable(1);
        self.UnitPrice = ko.pureComputed(function () {
            return self.Product() ? self.Product().Price : 0;
        });
        self.Cost = ko.pureComputed(function () {
            return self.Product() ? self.Product().Price * parseInt("0" + self.Quantity(), 10) : 0;
        });
        
            $.ajax({
                url: 'GetAllProducts',
                cache: false,
                type: 'GET',
                contentType: 'application/json; charset=utf-8',
                data: {},
                success: function (data) {
                    return self.Products(data); //Put the response in ObservableArray
                }
            });
    };

    var Sale = function () {
        var self = this;
        self.Id = ko.observable("");
        self.CreatedDate = ko.observable("");
        self.SaleDetails = ko.observableArray([new SaleDetail()]); // Contains the list of Sales line items
        self.GrossAmount = ko.pureComputed(function () {
            var total = 0;
            $.each(self.SaleDetails(), function () { total += this.Cost() })
            return total;
        });
        self.PaymentType = ko.observableArray(["Cash", "Credit"]);
        // Operations
        self.addLine = function () { self.SaleDetails.push(new SaleDetail()) };
        self.deleteLine = function (detailLine) { self.SaleDetails.remove(detailLine) };

        self.create = function () {
            //var dataToSave = $.map(self.SaleDetails(), function (line) {
            //    return line.Product() ? {
            //        productName: line.product().name,
            //        quantity: line.quantity()
            //    } : undefined
            //});
            var dataToSave = $.each(self.SaleDetails(), function (line) {
                return this
                //{
                //    ProductId: line.Product ? line.Product.Id : 0,
                //    Quantity : line.Quantity,
                //    UnitPrice : line.UnitPrice,
                //    Cost : line.Cost
                //}
            });
            if (dataToSave.length > 0) {
                $.ajax({
                    url: 'AddSale',
                    cache: false,
                    type: 'POST',
                    contentType: 'application/json; charset=utf-8',
                    data: ko.toJSON(self),
                    success: function (data) {
                        //self.Sales.push(data);
                        self.reset();
                    }
                }).fail(
                function (xhr, textStatus, err) {
                    alert(err);
                });
            }
            else {
                alert('Please Enter Products !!');
            }
        }
        self.reset = function () {
            self.CreatedDate("");
            self.PaymentType(["Cash", "Credit"]);
            self.SaleDetails([new SaleDetail()]);
        }
    };    

    

    //// Initialize the view-model
    //$.ajax({
    //    url: 'Sale/GetAllSales',
    //    cache: false,
    //    type: 'GET',
    //    contentType: 'application/json; charset=utf-8',
    //    data: {},
    //    success: function (data) {
    //        self.Sales(data); //Put the response in ObservableArray
    //    }
    //});
    //// Delete Sale details
    //self.delete = function (Sale) {
    //    if (confirm('Are you sure to Delete this Sale ??')) {
    //        var id = Sale.Id;

    //        $.ajax({
    //            url: 'Sale/DeleteSale/' + id,
    //            cache: false,
    //            type: 'POST',
    //            contentType: 'application/json; charset=utf-8',
    //            data: id,
    //            success: function (data) {
    //                self.Sales.remove(Sale);
    //            }
    //        }).fail(
    //        function (xhr, textStatus, err) {
    //            self.status(err);
    //        });
    //    }
    //}

    //// Edit Sale details
    //self.edit = function (Sale) {
    //    self.Sale(Sale);
    //}

    //// Update Sale details
    //self.update = function () {
    //    var Sale = self.Sale();

    //    $.ajax({
    //        url: 'Sale/EditSale',
    //        cache: false,
    //        type: 'PUT',
    //        contentType: 'application/json; charset=utf-8',
    //        data: ko.toJSON(Sale),
    //        success: function (data) {
    //            self.Sales.removeAll();
    //            self.Sales(data); //Put the response in ObservableArray
    //            self.Sale(null);
    //            alert("Record Updated Successfully");
    //        }
    //    })
    //    .fail(
    //    function (xhr, textStatus, err) {
    //        alert(err);
    //    });
    //}

    //// Cancel Sale details
    //self.cancel = function () {
    //    self.Sale(null);
    //}


//var viewModel = new SaleViewModel();
ko.applyBindings(new Sale());

function formatCurrency(value) {
    if (value != undefined && value != null)
        return value.toFixed(2);
    else
        return 0.00;
}

function formatQuantity(value) {
    if (value != undefined && value != null && value > 0)
        return value.toFixed(2);
    else
        return 0.00;
}
