
async function initMap(address) {
    try {
        if (address != undefined) {
            let html = null;
            let latlng = new google.maps.LatLng(19.412596570431628, -99.18741658511824);

            let options = {
                zoom: 19,
                center: latlng,
                mapTypeId: google.maps.MapTypeId.ROADMAP
                //mapTypeId: 'satellite'
            };

            let map = new google.maps.Map(document.getElementById('map'), options);

            //let marker = new google.maps.Marker({
            //    map: map,
            //    position: latlng,
            //    title: 'Latin Id'
            //});

            let geocoder = new google.maps.Geocoder();

            //geocoderAddress(geocoder, map, address);
            let promise = await geocoderAddressPromise(geocoder, map, address);
            if (promise) 
                html = document.getElementById('map').outerHTML;

            return html;
        }
    } catch (err) {
        return null;
    }
}

async function geocoderAddressPromise(geocoder, map, address) {
    return await new Promise((resolve) => {
        geocoder.geocode({ 'address': address }, function (results, status) {
            if (status === 'OK') {
                map.setZoom(19);
                map.setCenter(results[0].geometry.location);
                let marker = new google.maps.Marker({
                    map: map,
                    position: results[0].geometry.location
                });
            }
        }).then((result) => {
            if (result != null) {
                resolve(true);
            } else {
                resolve(false);
            }
        }).catch((err) => {
            resolve(false);
        });
    });
}

function geocoderAddress(geocoder, map, address) {
    geocoder.geocode({ 'address': address }, function (results, status) {
        if (status === 'OK') {
            map.setZoom(19);
            map.setCenter(results[0].geometry.location);
            let marker = new google.maps.Marker({
                map: map,
                position: results[0].geometry.location
            });
        }
    });
}

window.saveAsFile = function (fileName, byteBase64) {
    let link = this.document.createElement('a');
    link.download = fileName;
    link.href = "data:application/octet-stream;base64," + byteBase64;
    this.document.body.appendChild(link);
    link.click();
    this.document.body.removeChild(link);
}

function InfoBrowser() {
    var ua = navigator.userAgent, tem,
        M = ua.match(/(opera|chrome|safari|firefox|msie|trident(?=\/))\/?\s*(\d+)/i) || [];
    if (/trident/i.test(M[1])) {
        tem = /\brv[ :]+(\d+)/g.exec(ua) || [];
        return 'IE ' + (tem[1] || '');
    }
    if (M[1] === 'Chrome') {
        tem = ua.match(/\b(OPR|Edg)\/(\d+)/);
        if (tem != null) return tem.slice(1).join(' ').replace('OPR', 'Opera');
    }
    M = M[2] ? [M[1], M[2]] : [navigator.appName, navigator.appVersion, '-?'];
    if ((tem = ua.match(/version\/(\d+)/i)) != null) M.splice(1, 1, tem[1]);
    return M.join(' ');
};

function NombreNavegador() {
    let nombreNavegador = null;
    try {
        let infoBrowser = InfoBrowser();
        if (infoBrowser != null)
            nombreNavegador = infoBrowser.split(' ')[0];
        return nombreNavegador;
    } catch (err) {
        return null;
    }
}

async function ConsultarPortalListaNominal(url) {
    let mensaje = '';
    let nombreNavegador = NombreNavegador(); 
    if (nombreNavegador == 'Chrome') { 
        //window.open(url, "_blank", "location=0,status=0,menubar=0");
        window.open(url, "ListaNominal", "width=800,height=600,toolbar=no,location=no,status=no,menubar=no");
    } else {
        mensaje = 'Consulta de la lista nominal no soportado por este navegador web, asegurese de utilizar el portal web con Google Chrome.';
        mensaje = 'Para esta función debe utilizar el navegador web de Google Chrome.';
    }
    return mensaje;
}

var idTimerSesion = -1;
function TimerSesion(donetHelper) {
    try {
        clearTimeout(idTimerSesion);
        let segungosInactividad = 180;

        document.onmousemove = function () { resetTimer('OnMouseMove'); };
        document.onkeypress = function () { resetTimer('OnKeyPress'); };
        document.onclick = function () { resetTimer('OnClick'); };

        function resetTimer(tipoEvento) {
            //console.log(tipoEvento + ' Id del Timer: ' + idTimerSesion);
            clearTimeout(idTimerSesion);
            let inicio = new Date();
            idTimerSesion = setTimeout(logout, segungosInactividad * 1000, inicio, idTimerSesion);
        }

        function logout(inicio, idTimerOrigen) {
            let fin = new Date();
            let milisegundos = fin.getTime() - inicio.getTime();
            let segundos = milisegundos / 1000;
            console.log('Id del Timer cierre: ' + idTimerOrigen + ', Inicio: ' + inicio.toLocaleString() + ', fin: ' + fin.toLocaleString() + ', segundos:' + segundos);
            if (segundos > segungosInactividad) {
                TimerSesionDesactivar()
                donetHelper.invokeMethodAsync("Logout");
            }
        }
    } catch (error) {
        console.error(error);
    }
}

function TimerSesionDesactivar() {
    clearTimeout(idTimerSesion);
    document.onmousemove = null;
    document.onkeypress = null;
    document.onclick = null;
}

var progressAvanceLimite = 630;
var progressColorProgreso = '#168A00';
function MostrarScore(capa, avanceMaximo, avanceActual, tamano, colorFondo, colorAvance, simbolo, animar) {

    progressAvanceLimite = parseInt(avanceActual);

    let anchoFranja = 8;
    let descontar = 1.57;
    let DescuentoInicial = 630;
    let DescuentoMaximo = 156;
    let incremento = 1;

    switch (tamano) {
        case 60:
            anchoFranja = 8;
            DescuentoMaximo = 156;
            incremento = 1;
            break;
        case 110:
            anchoFranja = 16;
            DescuentoMaximo = 316;
            incremento = 10;
            break;
    }
    descontar = ((avanceActual * DescuentoMaximo) / avanceMaximo) / avanceActual;

    switch (colorAvance)
    {
        case 'verde':
            progressColorProgreso = '#168A00';
            break;
        case 'amarillo':
            progressColorProgreso = '#FEBF00';
            break;
        case 'rojo':
            progressColorProgreso = '#9F0002';
            break;
        case 'rojo2':
            progressColorProgreso = '#8B0000';
            break;
        default:
            progressColorProgreso = colorAvance;
            break;
    }

    let eCirculoBack = '.progress-circle-back';
    let eCirculo = '.progress-circle';
    let ePorcentaje = '.percentage-circle';

    let progressBack = capa.querySelector(eCirculoBack);
    let progressAvance = capa.querySelector(eCirculo);
    let porcentaje = capa.querySelector(ePorcentaje);

    progressBack.style.stroke = colorFondo;
    progressBack.style.strokeWidth = anchoFranja;
    progressAvance.style.stroke = progressColorProgreso;
    progressAvance.style.strokeWidth = anchoFranja;

    if (animar) {
        let incrementoProcesado = 0;
        let tiempo = setInterval(() => {
            incrementoProcesado += incremento;
            let value = Math.ceil(DescuentoInicial -= (descontar * incremento));
            porcentaje.textContent = incrementoProcesado + simbolo;
            progressAvance.style.strokeDashoffset = value;
            if (incrementoProcesado >= progressAvanceLimite) {
                porcentaje.textContent = avanceActual + simbolo;
                clearInterval(tiempo);
            }
        }, 40);
    } else {
        progressAvance.style.strokeDashoffset = DescuentoInicial - (descontar * avanceActual);
        porcentaje.textContent = progressAvanceLimite + simbolo;
    }

}

function FocusInput(id) {
    document.getElementById(id).focus();
}

function FocusInputLogin(idInputUsuario, idInputPassword) {

    try {
        let claseFoco = 'modified';
        let inputUsuario = document.getElementById(idInputUsuario);
        let inputPassword = document.getElementById(idInputPassword);
        if (!inputUsuario.classList.contains(claseFoco)) {
            inputUsuario.classList.add(claseFoco);
        }
        if (inputPassword.classList.contains(claseFoco)) {
            inputPassword.classList.remove(claseFoco);
        }
        inputUsuario.focus();
    } catch (error) {
        console.error(error);
    }
}

function InputDeleteChar(capa) {
    let longitud = capa.value.length - 1;
    let indice = capa.selectionStart - 1;
    if (longitud == indice) {
        capa.value = capa.value.substring(0, longitud);
    }
    else {
        let textoIzq = capa.value.substring(0, indice);
        let textoDer = capa.value.substring(indice + 1);
        capa.value = textoIzq + textoDer;
        capa.selectionStart = indice;
        capa.selectionEnd = indice;
    }
}

function InputValidateText(capa) {
    let text = capa.value;
    let dataText = text.replace(/[0-9]+/g, "");
    capa.value = dataText;
}

function InputValidateNumbers(capa) {
    let text = capa.value;
    let dataNumeric = text.replace(/[^0-9]+/g, "");
    capa.value = dataNumeric;
}


