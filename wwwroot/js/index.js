window.access = (url) => { window.location.href = url; }

window.OnRadioChange = (id) => {
    var radios = document.getElementsByClassName("radio");
    for(var i =0; i < radios.length; i++ )
    {
        if(radios[i].id == id) radios[i].checked = true;
        else radios[i].checked = false;
    }
}