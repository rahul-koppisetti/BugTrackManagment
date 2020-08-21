// Please see documentation at https://docs.microsoft.com/aspnet/core/client-side/bundling-and-minification
// for details on configuring this project to bundle and minify static web assets.

// Write your JavaScript code.


//global
//for spinner
$(function () {
    $("#loaderbody").addClass('hide');
    $(document).bind('ajaxStart', function () {
        $("#loaderbody").removeClass('hide');
    }).bind('ajaxStop', function () {
        $("#loaderbody").addClass('hide');
    });
});

//my notify
msgnotify = (msg, type) => {

    Toast.fire({
        icon: type,
        title: msg
    })
}

const Toast = Swal.mixin({
    toast: true,
    position: 'top-end',
    showConfirmButton: false,
    timer: 3000,
    timerProgressBar: true,
    onOpen: (toast) => {
        toast.addEventListener('mouseenter', Swal.stopTimer)
        toast.addEventListener('mouseleave', Swal.resumeTimer)
    }
})




//modal
showInPopup = (url, title) => {
    $.ajax({
        type: "Get",
        url: url,
        success: function (res) {
            $('#issue-modal .modal-title').html(title);
            $('#issue-modal .modal-body').html(res);
            $('#issue-modal').modal('show');
        }
    })
}





//modal form
showform = (url, title) => {
    $.ajax({
        type: "Get",
        url: url,
        success: function (res) {
            $('#createform-modal .modal-title').html(title);
            $('#createform-modal .modal-body').html(res);
            $('#createform-modal').modal('show');
            
        }
    })
}

//tester methods
//post delete
jQueryAjaxDelete = form => {
     
   
        Swal.fire({
            title: 'Are you sure?',
            text: "You won't be able to revert this!",
            icon: 'warning',
            showCancelButton: true,
            confirmButtonColor: '#d33',
            cancelButtonColor: '#3085d6',
            confirmButtonText: 'Yes, delete it!'
        }).then((result) => {
            if (result.value) {



                $.ajax({
                    type: 'POST',
                    url: form.action,
                    data: new FormData(form),
                    contentType: false,
                    processData: false,
                    success: function (res) {

                        
                        //$('#_index').html(res.html); 
                        msgnotify('Successfully Deleted ', 'warning');
                        
                    },
                    error: function (err) {
                        console.log(err)
                    }
                })
            } 

                
            
        })

           
    //prevent default form submit event
    return false;
}

//Verify marking

ajaxmarkverify = (form,title) => {

            $.ajax({
                type: 'POST',
                url: form.action,
                data: new FormData(form),
                contentType: false,
                processData: false,
                success: function (res) {

                    //$('#_index').html(res.html);
                    msgnotify(title + ' is marked as Verfied ', 'success');
                    $('#createform-modal .modal-body').html('');
                    $('#createform-modal .modal-title').html('');
                    $('#createform-modal').modal('hide');
                },
                error: function (err) {
                    console.log(err)
                }
            })
      


    //prevent default form submit event
    return false;
    
}


updateAllTeser = () => {


}


//post create issue
jQueryAjaxPost = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData( form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {
                    

                    //$('#Index_BugListViewComponent').html(res.html);
                    $('#createform-modal .modal-body').html('');
                    $('#createform-modal .modal-title').html('');
                    $('#createform-modal').modal('hide');

                    msgnotify('Successfully Created ', 'success');

                }
                else
                    $('#createform-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}



//post Edit issue
jQueryAjaxPut = form => {
    try {
        $.ajax({
            type: 'POST',
            url: form.action,
            data: new FormData(form),
            contentType: false,
            processData: false,
            success: function (res) {
                if (res.isValid) {


                    //$('#Index_BugListViewComponent').html(res.html);
                    $('#issue-modal  .modal-body').html('');
                    $('#issue-modal .modal-title').html('');
                    $('#issue-modal').modal('hide');

                    msgnotify('Successfully Edited ', 'success');

                }
                else
                    $('#issue-modal .modal-body').html(res.html);
            },
            error: function (err) {
                console.log(err)
            }
        })
        //to prevent default form submit event
        return false;
    } catch (ex) {
        console.log(ex)
    }
}




$("#buglistlink").click(function () {
    $("#buglist").toggle("highlight", { color: "#008000" }, 3000);
});