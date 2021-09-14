(function () {

    var WebGetPriorOrderCountByUserId = "/WebServices/OrderWebService.asmx/GetPriorOrderCountByUserId";
    var WebGetProductOptionsById = "/WebServices/OrderWebService.asmx/GetProductOptionsById";
    var WebGetAvailableProducts = "/WebServices/OrderWebService.asmx/GetAvailableProducts";

    var _userId;
    var _productId;
    var _isAnnual = false;
    var _isTrialAllowed = true;
    var _productOptions;
    var _orderCount = 0;
    var _allProducts;

    var PackagePrefs = {
        init: function (userId, productId, isAnnual) {
            _userId = userId;
            _productId = productId;
            _isAnnual = isAnnual;

            if (_userId != undefined && _userId > 0) {
                this.loadPriorOrderCounts();
                this.getAvailableProductOptions();
            }

            this.getAvailableProducts();
        },

        loadPriorOrderCounts: function () {
            $.ajax({
                type: "POST",
                url: WebGetPriorOrderCountByUserId,
                cashe: false,
                data: '{"userId":' + _userId + '}',
                dataType: "json",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    _orderCount = response.d;

                    _isTrialAllowed = (_orderCount == undefined || _orderCount == 0);
                },
                complete: function (response) {
                }
            });
        },

        getAvailableProductOptions: function () {
            var annual = _isAnnual == 'False' ? false : true;
            $.ajax({
                type: "POST",
                url: WebGetProductOptionsById,
                cashe: false,
                data: '{"userId":' + _userId + ', "productId":' + _productId + ', "isAnnual":' + annual + '}',
                dataType: "json",
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    _productOptions = response.d;

                    var with_trial = '14-Day Free Trial. You will not be charged anything if cancel your plan before your 14-day free trial expires.</br> It is free for 14 days, then starting at US$';
                    var no_trial = 'You have already used your 14-day trial. You will be charged US$';
                    var msg_end = '.</br>You can upgrade, downgrade, or cancel your subscription at any time.';

                    if (_productOptions != undefined) {
                        
                        if (annual) {
                            $('#packagePrice').html('$' + _productOptions.AnnualPrice.toFixed(2));
                            $("#byClickingPrice").html('$' + _productOptions.AnnualPrice.toFixed(2)+" (annual subscription)")
                            if (_isTrialAllowed) {
                                $('#subMsg').html(with_trial + _productOptions.AnnualPrice.toFixed(2) + '/year' + msg_end);
                            }
                            else {
                                $('#subMsg').html(no_trial + _productOptions.AnnualPrice.toFixed(2) + '/year' + msg_end);
                            }
                        }
                        else {
                            $('#packagePrice').html('$' + _productOptions.Price);
                            if (_productOptions.ProductId == 1)
                            {
                                $("#byClickingPrice").html('$' + _productOptions.Price + "/month/channel (monthly subscription)")
                            }
                            else
                            {
                                $("#byClickingPrice").html('$' + _productOptions.Price + "/month (monthly subscription)")
                            }
                          
                            if (_isTrialAllowed) {
                                $('#subMsg').html(with_trial + _productOptions.Price + '/month' + msg_end);
                            }
                            else {
                                $('#subMsg').html(no_trial + _productOptions.Price + '/month' + msg_end);
                            }
                        }
                    }
                },
                complete: function (response) {
                }
            });
        },

        getAvailableProducts: function () {
            $.ajax({
                type: "POST",
                url: WebGetAvailableProducts,
                cashe: false,
                async: false,
                contentType: "application/json; charset=utf-8",
                success: function (response) {
                    _allProducts = response.d;
                    void 0;
                },
                complete: function (response) {
                }
            });
        },

        getMonthlyProductPrice: function(productId) {
            var price = 0;

            if (_allProducts != null && _allProducts != undefined) {
                for (var i=0; i < _allProducts.length; i++) {
                    var product = _allProducts[i];
                    if (product.ProductId == productId) {
                        price = product.Price;
                    }
                }
            }

            return price;
        },

        getAnnualProductPrice: function(productId) {
            var price = 0;
            if (_allProducts != null && _allProducts != undefined) {
                for (var i=0; i < _allProducts.length; i++) {
                    var product = _allProducts[i];
                    if (product.ProductId == productId) {
                        price = product.AnnualPrice;
                    }
                }
            }
            
            return price;
        },

        setMonthlyPricesOnPackages: function() {
            this.setPackagePricing(1, true);
            this.setPackagePricing(5, true);
            this.setPackagePricing(4, true);
            this.setPackagePricing(7, true);
        },

        setPackagePricing: function(packageId, isMonthly) {
            var price = 0;
            var period = '/month';

            if (isMonthly) {
                price = this.getMonthlyProductPrice(packageId);
            }
            else {
                price = this.getAnnualProductPrice(packageId);
                period = '/year';
                price = price.toFixed(2);
               
            }

            if (packageId == 1) {
                $('.basicPkgPrice').html('$' + price + period);
                $('#basicPkgPriceMobile').html('$' + price + period);
            }
            else if (packageId == 5) {
                $('.advPkgPrice').html('$' + price + period);
                $('#advPkgPriceMobile').html('$' + price + period);
            }
            else if (packageId == 4) {
                $('.profPkgPrice').html('$' + price + period);
                $('#profPkgPriceMobile').html('$' + price + period);
            }
            else if (packageId == 7) {
                $('.profplusPkgPrice').html('$' + price + period);
                $('#profplusPkgPricemobile').html('$' + price + period);
            }
        },

        isTrialAllowed: function () {
            return _isTrialAllowed;
        },

        isAnnualSubscription: function () {
            return _isAnnual;
        },

        getSubscriberId: function () {
            return _productOptions.SubscriptionButtonId;
        }
    };

    getPreferences = function () {
        return PackagePrefs;
    };
})();
