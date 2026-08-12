$.fn.optgroupTrans = function() {

        var items = $(this);
        var groupnames = [];
        for (var i = 0; i < items.length; i++) {

            if ($(items[i]).attr('optgroup') != null) {
                groupnames.push($(items[i]).attr('optgroup'));
            }
        }

        // groupnames = $.unique(groupnames);
        groupnames = uniqueArray(groupnames);
        for (var i = 0; i < groupnames.length; i++) {
            $("option[optgroup='" + groupnames[i] + "']").wrapAll("<optgroup label='" + groupnames[i] + "'>");
        }


    function uniqueArray(a){
        temp = new Array();
        for(var i = 0; i < a.length; i++){
            if (!contains(temp, a[i])){
                temp.length += 1;
                temp[temp.length - 1] = a[i];
            }
        }
        return temp;
    }
    function contains(a, e) {
        for (j = 0; j < a.length; j++) 
              if (a[j] == e) return true;
        return false;
    }

};

