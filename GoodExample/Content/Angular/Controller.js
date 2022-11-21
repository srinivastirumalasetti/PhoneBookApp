app.controller("myCntrl", function ($scope, $http, $window, angularService) {

    $scope.btnAddContacts = true;
    $scope.type = "hidden";
    
    ////To Get All Phone book Records
    $scope.GetAllPhoneBookList  = function () {
        
        GetAllContacts();      
    }

    ////To fetch all contacts
    function GetAllContacts() {
       
        var getData = angularService.getPhoneBookList();
        getData.then(function (cnt) {
           
            $scope.contactslist = eval(cnt.data);
                                  
            $scope.curPage = 1;
            $scope.itemsPerPage = 3;
            $scope.maxSize = 5;

            $scope.numOfPages = function () {
                return Math.ceil($scope.contactslist.length / $scope.itemsPerPage);
            };
            $scope.$watch('curPage + numPerPage', function () {
                
                var begin = (($scope.curPage - 1) * $scope.itemsPerPage),
                end = begin + $scope.itemsPerPage;
                $scope.PagerContacts = $scope.contactslist ? $scope.contactslist.slice(begin, end) : ''; 
            });

        }, function () {
            alert('No data found');
        });
    }    
  

    // add Contact
    $scope.addContact = function () {
                  
        var Contact = {
            phoneID: 0,
            firstname: this.contact.firstname,
            lastname: this.contact.lastname,
            phonenumber: this.contact.phonenumber
        };

        //Validate PhoneNumber
        var getPhoneData = angularService.validatephonenumber(Contact.phonenumber);
        getPhoneData.then(function (ct) {

            if (ct.data == "0") {

                var getData = angularService.AddContact(Contact);
                getData.then(function (msg) {
                    $scope.contact = null;
                    alert(msg.data);
                    $('#contactModel').modal('hide');
                    GetAllContacts();
                    
                }, function (response) {
                    if (response) {
                        alert(response.data);
                    }
                });
            }
            else {
                alert("Phone number already exists");

            }

        }, function (response) {
            if (response) {
                alert(response.data);
            }

        });
     
    };

    //update contact
    $scope.updateContact = function () {
     
        var contact = this.contact;
               
        var getData = angularService.updateContact(contact);
        getData.then(function (msg) {
            alert(msg.data);
            $scope.contact = null;
            contact.edit = false;
            $('#contactModel').modal('hide');
            GetAllContacts();
        }, function (response) {
            if (response) {
                alert(response.data);
            }

        });
      
    };

    // delete Contact
    $scope.deleteContact = function () {
        
        var ct = $scope.contact;
        var getData = angularService.DeleteContact(ct);
        getData.then(function (msg) {
            alert('Contact Deleted');
            $('#confirmModal').modal('hide');
            GetAllContacts();           
            
        }, function () {
            alert('Error in Deleting Record');
        });
    }
    
    //Model popup events
    $scope.showadd = function () {
        $scope.editMode = false;
        $scope.contact = null;
        $('#contactModel').modal('show');
    };

    $scope.showedit = function () {
        $('#contactModel').modal('show');
    };
    
    $scope.cancel = function () {
        $scope.contact = null;
        $('#contactModel').modal('hide');
        GetAllContacts();
    }
          
    //edit contact
    $scope.edit = function (data) {
                
        $scope.contact = data;
        $scope.editMode = true;
        $('#contactModel').modal('show');
    };
    
    $scope.showconfirm = function (data) {
        $scope.contact = data;
        $('#confirmModal').modal('show');
    };

    
});