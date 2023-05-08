var ManagerSearchBar = function () {

    var search = function (e) {
        let filterValue = e.target.value.toUpperCase();

        let elements = document.querySelectorAll('.elements tr');
        for (let i = 0; i < elements.length; i++) {
            let textValue = $(elements[i].querySelectorAll('td:not(:last-child)')).text();
            if (textValue.toUpperCase().indexOf(filterValue) > -1) {
                elements[i].style.display = '';
            } else {
                elements[i].style.display = 'none';
            }
        }
    }

    var init = function () {
        $('#search--js').on('keyup', search);
    }

    return {
        init: init
    }
}();