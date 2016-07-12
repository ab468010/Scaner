function useContainer() {
    (function () {
        $.ajax({
            type: "post",
            url: Globals.ServiceUrl + "GetUseContainerList",
            contentType: "application/json; charset=utf-8",
            success: function (data) {
                var s = JSON.parse(data.d);
                var tbody=$(".table tbody").empty();
                for(var i in s){
                    var cont = "<ta>" + s[i].Name + "</td><td>" + s[i].ContainerCode + "</td><td>" + s[i].Size + "</td><td style='display:none'>" + s[i].ContainerId + "</td>";
                    var row = document.createElement("tr");
                    row.innerHTML = cont;
                    tbody.append(row);
                }
               
            }
        })
    })()
}
$(function () {
    useContainer();
})