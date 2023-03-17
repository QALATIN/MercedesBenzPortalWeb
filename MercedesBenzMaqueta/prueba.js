
mostrarScore();

// let progress = document.getElementById("progress");
// let porcentaje = document.getElementById("porcentaje");
// let cantidad1 = 0;
// let cantidad2 = 630;

// let tiempo = setInterval(() =>{
//     cantidad1+=1;
//     let value = Math.ceil(cantidad2-=1.54);
//     porcentaje.textContent = cantidad1+'%';
//     progress.style.strokeDashoffset=value;
//     if(cantidad1 == 90)
//     {
//         clearInterval(tiempo);
//     }
// }, 40);

function mostrarScore() {

    let size = 110;
    let strokeBack = '#EAEAEA';
    let stroke = '#168A00';
    let avanceMaximo = 100;
    let avanceActual = 50;

    let strokeWidth = 8;
    let descontar = 1.57;
    let incremento = 1;
    let porcentajeLimite = 109;
    let DescuentoInicial = 630;
    let DescuentoMinimo = 474;
    let timerEspera = 40;

    // let porcentajeLimite = (109/1000)*100;
    // let porcentajeLimite2 = (55/100)*100;

    switch(size) {
        case 60:
            strokeWidth = 8;
            break;
        case 110:
            strokeWidth = 16;
            avanceMaximo = 1000;
            avanceActual = 109;
            DescuentoMinimo = 316;
            incremento = 10;
            // timerEspera = 10;
            break;
    }
    descontar = ((avanceActual*DescuentoMinimo)/avanceMaximo)/avanceActual;

    let capa = document.querySelector('#ProgressScore2');

    let eCirculoBack = '.progress-circle-back';
    let eCirculo = '.progress-circle';
    let ePorcentaje = '.percentage-circle';

    let progressBack = capa.querySelector(eCirculoBack);
    let progress = capa.querySelector(eCirculo);
    let porcentaje = capa.querySelector(ePorcentaje);
    
    progressBack.style.stroke = strokeBack;
    progressBack.style.strokeWidth = strokeWidth;
    progress.style.stroke = stroke;
    progress.style.strokeWidth = strokeWidth;

    let cantidad1 = 0;
    
    let tiempo = setInterval(() =>{
        cantidad1+=incremento;
        let value = Math.ceil(DescuentoInicial-=(descontar*incremento));
        porcentaje.textContent = cantidad1+'%';
        progress.style.strokeDashoffset=value;
        if(cantidad1 >= porcentajeLimite)
        {
            porcentaje.textContent = avanceActual+'%';
            clearInterval(tiempo);
        }
    }, 40);
}

function mostrarPassword(){
    var cambio = document.getElementById("txtPassword");
    if(cambio.type == "password"){
        cambio.type = "text";
        $('.icon').removeClass('fa fa-eye-slash').addClass('fa fa-eye');
    }else{
        cambio.type = "password";
        $('.icon').removeClass('fa fa-eye').addClass('fa fa-eye-slash');
    }
} 

// $(document).ready(function () {
// //CheckBox mostrar contraseña
// $('#ShowPassword').click(function () {
//     $('#Password').attr('type', $(this).is(':checked') ? 'text' : 'password');
// });
// });
