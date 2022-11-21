app.service("angularService", function ($http) {

    
    // Validate phone number
    this.validatephonenumber = function (phonenumber) {
        return $http.get('api/Default/' + phonenumber);
               
    }
   
    //////get All contacts
    this.getPhoneBookList = function () {
          
       return $http({
            method: 'GET',
            url: "api/Default",
            headers: {
                "Content-Type": "application/json;charset=UTF-8"                
            }
        });
    };
        
    // Add Contact
    this.AddContact = function (Contact) {

        var response = $http({
            method: "POST",
            url: "api/Default/",
            data: JSON.stringify(Contact),
            headers: {
                "Content-Type": "application/json;charset=UTF-8"
                
            }
        });
        return response;
    }

    // Update Contact
    this.updateContact = function (contact) {
        var response = $http({
            method: "put",
            url: "api/Default",
            data: JSON.stringify(contact),
            dataType: "json"
        });
        return response;
    }

    //Delete Contact
    this.DeleteContact = function (contact) {
    
        var response = $http({
            method: "DELETE",
            url: "api/Default",
            data: JSON.stringify(contact),
            headers: {
                "Content-Type": "application/json;charset=UTF-8"

            }
           
        });
        return response;
    }
 
});