
var xEmp = 2; var yEmp = 2; var T = 50;
var lineV; var lineH; var rectangle;

var images = [
    { key: 'espacio', value: 'http://localhost:52124/Content/img/espacio.png', width: T, height: T, },
    { key: 'arrow_right', value: 'http://localhost:52124/Content/img/arrow_right.png', width: T, height: T, },
    { key: 'arrow_left', value: 'http://localhost:52124/Content/img/arrow_left.png', width: T, height: T, },
    { key: 'item', value: 'http://localhost:52124/Content/img/diente.png', width: T, height: T, },
    { key: 'panel', value: 'http://localhost:52124/Content/img/panel.png', width: T, height: T, },
    { key: 'save_button', value: 'http://localhost:52124/Content/img/save_button.png', width: T, height: T, },
    { key: 'print_button', value: 'http://localhost:52124/Content/img/print_button.png', width: T, height: T, },
    { key: 'toggle_d', value: 'http://localhost:52124/Content/img/toggle_d.png', width: T, height: T, },
    { key: 'toggle_ed', value: 'http://localhost:52124/Content/img/toggle_ed.png', width: T, height: T, },
    { key: 'background', value: 'http://localhost:52124/Content/img/background.png', width: T, height: T, },
];

var init_config = {
    "dientes": [],
    "sim_defaults": [],
    "simbolos": [],
    "espacios": [],
    /*"dientes_incompatibles": [
        { diente1: "11", diente2: "51" },
        { diente1: "12", diente2: "52" },
        { diente1: "13", diente2: "53" },
        { diente1: "14", diente2: "54" },
        { diente1: "15", diente2: "55" },

        { diente1: "21", diente2: "61" },
        { diente1: "22", diente2: "62" },
        { diente1: "23", diente2: "63" },
        { diente1: "24", diente2: "64" },
        { diente1: "25", diente2: "65" },

        { diente1: "31", diente2: "71" },
        { diente1: "32", diente2: "72" },
        { diente1: "33", diente2: "73" },
        { diente1: "34", diente2: "74" },
        { diente1: "35", diente2: "75" },

        { diente1: "41", diente2: "81" },
        { diente1: "42", diente2: "82" },
        { diente1: "43", diente2: "83" },
        { diente1: "44", diente2: "84" },
        { diente1: "45", diente2: "85" },
    ]*/
};


var game = new Phaser.Game(21 * T, 11 * T, Phaser.CANVAS, 'odontograma', { preload: preload, create: create, render: render, update: update });

function preload() {

    var id = $("#iOdi").val();

    $.ajax({
        async: false,
        url: '/OdontogramaDetalle/Detalle',
        method: 'post',
        data: JSON.stringify({ id: id }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            //debugger
            if (data.length != 0) {
                init_config.simbolos = data;
            }
        },
        error: function (ex) {
            //debugger
            alert(ex.responseText);
        }
    });

    $.ajax({
        async: false,
        url: '/OdontogramaDetalle/Simbolos',
        method: 'post',
        //data: JSON.stringify({ id: id }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            //debugger
            if (data.length != 0) {
                init_config.sim_defaults = data;
            }
        },
        error: function (ex) {
            //debugger
            alert(ex.responseText);
        }
    });

    init_config.sim_defaults.forEach(o => {
        images.push({ key: o.sDescripcion, value: o.sPathImage, width: T, height: T, tipo: o.tipo});
    });

    //this.load.crossOrigin = 'Anonymous';
    images.forEach(o => {
        game.load.image(o.key, o.value, o.width, o.height);
    });

    //  Load the Google WebFont Loader script
    game.load.script('webfont', '/Scripts/US/webfont.js');

}

var grupo_dientes;
var grupo_simbolos;
var grupo_tools;
var grupo_espacios;
var switcher_bool;
var switcher;
var save_button;
var print_button;
//var descriptionText;

function create() {

    game.add.image(0, 0, 'background');
    save_button = game.add.sprite(0.2 * T, 0.2 * T, 'save_button');
    print_button = game.add.sprite(0.4 * T + 35, 0.2 * T, 'print_button');
    var arrow_derecha = game.add.sprite(14 * T, T * (yEmp + 7), 'arrow_right');
    var arrow_izquierda = game.add.sprite(5 * T, T * (yEmp + 7), 'arrow_left');
    save_button.inputEnabled = true;
    print_button.inputEnabled = true;
    arrow_derecha.inputEnabled = true;
    arrow_izquierda.inputEnabled = true;
    save_button.input.useHandCursor = true;
    print_button.input.useHandCursor = true;
    arrow_derecha.input.useHandCursor = true;
    arrow_izquierda.input.useHandCursor = true;

    // evento click de boton Save
    print_button.events.onInputUp.add(print_up, this);
    save_button.events.onInputUp.add(save_up, this);
    arrow_derecha.events.onInputUp.add(fn_arrowDerecha, this);
    arrow_izquierda.events.onInputUp.add(fn_arrowIzquierda, this);

    //L�neas divisorias
    lineV = new Phaser.Line(T * (xEmp + 8), T * yEmp, T * (xEmp + 8), T * (yEmp + 4) + 3);
    lineH = new Phaser.Line(T * xEmp, T * (yEmp + 2) + 3, T * (xEmp + 16), T * (yEmp + 2) + 3);


    //Switcher toggle of panel
    switcher = game.add.sprite(T * (xEmp + 3), T * (yEmp + 6), 'toggle_d');
    switcher.inputEnabled = true;
    switcher.input.useHandCursor = true;
    switcher.events.onInputUp.add(fn_switcher, this);
    switcher.data.state = true;


    //Panel de opciones
    game.add.sprite(T * (xEmp + 4), T * (yEmp + 6), 'panel');

    //Texto descriptivo de s�mbolos del panel de opciones
    /*descriptionText = game.add.text(T * (xEmp + 3) + 60, T * (yEmp + 6) - 15, '');
    descriptionText.font = 'Arial';
    descriptionText.fontSize = 15;*/
    
    //Crear grupos de sprites para recorrerlos a futuro
    grupo_dientes = game.add.group(); //back layer
    grupo_espacios = game.add.group(); //mid layer
    grupo_tools = game.add.group(); //front layer
    grupo_simbolos = game.add.group(); //mid layer
    

    //Espacios para abreviaturas
    for (var i = 1; i <= 16; i++) {
        addAbreviatura(i, -1);
        addAbreviatura(i, 4);
    }

    //Agregando los dientes
    for (var i = 1; i <= 16; i++) {
        addDiente(i, 1);
        if (i > 3 && i <= 13) {
            addDiente(i, 2);
            addDiente(i, 3);
        }
        addDiente(i, 4);
    }

    

    llenarPanel(0);

}



function fn_arrowDerecha() { llenarPanel(+1); }
function fn_arrowIzquierda() { llenarPanel(-1); }
function fn_switcher() {
    //debugger
    if (switcher.data.state == true) {
        switcher.loadTexture('toggle_ed', 0);
    }
    else {
        switcher.loadTexture('toggle_d', 0);
    }
    switcher.data.state = !switcher.data.state;
    llenarPanel(0);
}

var public_pagina = 1;
var encontrado = 0;

function llenarPanel(increment) {
    //debugger
    //Si en la pagina : public_pagina + increment, no hay nada, entonces no hacer nada
    encontrado = 0;

    if (switcher.data.state == true) {
        init_config.sim_defaults.forEach(o => {
            init_config.sim_defaults.forEach(o => {
                if (o.tipo==="T") {
                    if (o.nPagina == (public_pagina + increment)) {
                        encontrado = 1;
                    }
                }
            });
        });
    }
    else {
        init_config.sim_defaults.forEach(o => {
            if (o.tipo === "A") {
                if (o.nPagina == (public_pagina + increment)) {
                    encontrado = 1;
                }
            }
        });
    }
    

    if (!encontrado)
        return;

    public_pagina += increment;

    //debugger
    //Borrar elementos actuales del panel
    for (i = 0; 10 > i; i++) { //Ciclo porque pasaba de que si se hace solo una vez, no se borran todos (bug de phaser)
        grupo_tools.forEach(function (it) {
            it.destroy();
        });
    }

    //debugger
    //Agregar los simbolos disponibles al panel
    i = 4;
    j = 6;
    //debugger
    if (switcher.data.state == true) {
        init_config.sim_defaults.forEach(o => {
            //debugger
            if (o.tipo === "T") {
                if (o.nPagina == public_pagina) {
                    item = grupo_tools.create(T * (xEmp + i), T * (yEmp + j), o.sDescripcion);
                    item.inputEnabled = true;
                    item.input.useHandCursor = true;
                    item.input.enableSnap(T, T, false, true);
                    item.events.onInputDown.add(DuplicateAndDrag, this);
                    //item.events.onInputOver.add(SimboloOver, this);
                    //item.events.onInputOut.add(SimboloOut, this);
                    item.data = o;
                }
                j++;
                if (j == 9) {
                    j = 6;
                    i++;
                }
                if (i == 12) {
                    i = 4;
                    j = 6
                }
            }
        });
    }
    else {
        init_config.sim_defaults.forEach(o => {
            if (o.tipo === "A") {
                if (o.nPagina == public_pagina) {
                    item = grupo_tools.create(T * (xEmp + i), T * (yEmp + j), o.sDescripcion);
                    item.inputEnabled = true;
                    item.input.useHandCursor = true;
                    item.input.enableSnap(T, T, false, true);
                    item.events.onInputDown.add(DuplicateAndDrag, this);
                    //item.events.onInputOver.add(SimboloOver, this);
                    //item.events.onInputOut.add(SimboloOut, this);
                    item.data = o;
                }
                j++;
                if (j == 9) {
                    j = 6;
                    i++;
                }
                if (i == 12) {
                    i = 4;
                    j = 6
                }
            }
        });
    } 
}


/*function SimboloOver(item) {
    descriptionText.text = item.data.sDescripcionLarga;
}
function SimboloOut(item) {
    descriptionText.text = "";
}*/

function save_up() {
    //console.log(init_config.simbolos);
    //Armar JSON leyendo las posiciones de cada diente, y si tiene enfermedades (simbolos) puestos encima, agregarlo al array

    console.log(JSON.stringify(init_config.simbolos));

    //debugger
    $.ajax({
        async: false,
        url: '/OdontogramaDetalle/SaveOdontogramaDetalle',
        method: 'post',
        data: JSON.stringify({ json: JSON.stringify(init_config.simbolos) }),
        dataType: 'json',
        contentType: 'application/json',
        success: function (data) {
            /*if (data.length != 0) {
                init_config.simbolos = data;
            }*/
            alert('Success '+data);
        },
        error: function (ex) {
            alert(ex.responseText);
        }
    });
    
}

function print_up() {
    var new_canvas = document.createElement("canvas");
    new_canvas.width = 1050;
    new_canvas.height = 360;

    var canvas_get = document.getElementsByTagName('canvas')[0];

    var ctx = new_canvas.getContext("2d");
    ctx.drawImage(canvas_get, 0, 0);

    var dataUrl = new_canvas.toDataURL();
    var windowContent = '<!DOCTYPE html>';
    windowContent += '<html>'
    windowContent += '<head><title>Print canvas</title></head>';
    windowContent += '<body>'
    windowContent += $('#info_paciente').html();
    windowContent += '<img src="' + dataUrl + '">';
    windowContent += '</body>';
    windowContent += '</html>';
    var printWin = window.open('', '', 'width=1050,height=500');
    printWin.document.open();
    printWin.document.write(windowContent);
    printWin.document.close();
    printWin.focus();
    printWin.print();
    printWin.close();
    return;
}

function render() {

    /*game.debug.text('X: '+game.input.mousePointer.x, 100, 560);
    game.debug.text('Y: '+game.input.mousePointer.y, 280, 560);*/
    game.debug.geom(lineV, '#000');
    game.debug.geom(lineH, '#000');

}

function update() {


}

/*function Posicion(item){
    text.text = ':: '+item.data.x+','+item.data.y;
}*/


function DuplicateAndDrag(item) {

    var tmpItem = grupo_simbolos.create(game.input.mousePointer.x, game.input.mousePointer.y, item.generateTexture());
    
    tmpItem.inputEnabled = true;
    tmpItem.input.enableDrag();
    tmpItem.input.enableSnap(T, T, false, true);
    //tmpItem.anchor.setTo(0.5);
    tmpItem.input.startDrag(game.input.activePointer);
    tmpItem.events.onDragStop.add(ValidationDrop, this);
    tmpItem.events.onInputUp.add(ValidationDrop, this);

    var tipo_nuevo = "";
    if (item.data.tipo === "T")
        tipo_nuevo = "S";
    if (item.data.tipo === "A")
        tipo_nuevo = "V";

    tmpItem.data = { tipo: tipo_nuevo, sidentifier: uniqueID(), nOdontogramaDetalleID: 0, sNombreDiente: null, sDescripcion: item.key };
    init_config.simbolos.push(tmpItem.data);
}

function ValidationDrop(item, pointer) {
    debugger
    //validating: 1 -- que no se busque a si mismo
    //tipo: S -- simbolo
    item.data.validating = 1;
    var encontrado = 0;
    //debugger
    if (item.data.tipo === "S") {
        grupo_dientes.forEach(function (it) {
            debugger
            if (it.x == item.x && it.y == item.y && it.data.validating != 1) {
                encontrado = 1;
                item.data.validating = 0;

                if (it.data.Nombre != undefined)
                    item.data.sNombreDiente = it.data.Nombre;

                //Encontrar si existe, el nOdontogramaDetalleID, modificar los valores
                init_config.simbolos.forEach(o => {
                    debugger
                    if (o.nOdontogramaDetalleID == item.data.nOdontogramaDetalleID && o.sidentifier === item.data.sidentifier) {
                        if (item.data.sNombreDiente != undefined) {
                            o.sNombreDiente = item.data.sNombreDiente;
                            console.log(o.sNombreDiente);
                        }
                    }
                });

            }
        });
    }
    if (item.data.tipo === "V") {
        grupo_espacios.forEach(function (it) {
            if (it.x == item.x && it.y == item.y && it.data.validating != 1) {
                encontrado = 1;
                item.data.validating = 0;

                if (it.data.Nombre != undefined)
                    item.data.sNombreDiente = it.data.Nombre;

                //Encontrar si existe, el nOdontogramaDetalleID, modificar los valores
                init_config.simbolos.forEach(o => {
                    debugger
                    if (o.nOdontogramaDetalleID == item.data.nOdontogramaDetalleID && o.sidentifier === item.data.sidentifier) {
                        if (item.data.sNombreDiente != undefined) {
                            o.sNombreDiente = item.data.sNombreDiente;
                            console.log(o.sNombreDiente);
                        }
                    }
                });

            }
        });
    }
    
    if (encontrado == 0) {
        //Eliminar del init_config la primer ocurrencia del nOdontogramaDetalleID, sNombreDiente, sDescripcion del item
        init_config.simbolos.forEach(o => {
            if (o.sidentifier===item.data.sidentifier && o.nOdontogramaDetalleID === item.data.nOdontogramaDetalleID && o.sNombreDiente === item.data.sNombreDiente && o.sDescripcion === item.data.sDescripcion) {
                init_config.simbolos.splice(init_config.simbolos.indexOf(o), 1); //Eliminar el elemento current
            }
        });
        item.destroy();
    }
    else {
        debugger
        //Diente encontrado, entonces validar compatibilidad con demas simbolos
        var diente = item.data.sNombreDiente; //Diente sobre el cual se suelta el s�mbolo
        var incompatibilidad_encontrada = 0;

        /*if (!item.data.sNombreDiente.includes("E")) {

            var diente_actual = parseInt(diente); //Convertimos a entero
            var diente_incompatible = (diente_actual < 51) ? diente_actual + 40 : diente_actual - 40;

            init_config.simbolos.forEach(o => {
                if (o != undefined) {
                    if (!o.sNombreDiente.includes("E")) {
                        if (o.sNombreDiente === diente_incompatible.toString() && o.sidentifier != item.data.sidentifier) {
                            debugger
                            incompatibilidad_encontrada = 1;
                            alert("Incompatibilidad de dientes encontrada");
                        }
                    }
                }
            });
        }

        if (incompatibilidad_encontrada) {
            debugger
            //Eliminar del init_config la primer ocurrencia del nOdontogramaDetalleID, sNombreDiente, sDescripcion del item
            init_config.simbolos.forEach(o => {
                debugger
                if (o.sidentifier === item.data.sidentifier && o.nOdontogramaDetalleID === item.data.nOdontogramaDetalleID && o.sNombreDiente === item.data.sNombreDiente && o.sDescripcion === item.data.sDescripcion) {
                    init_config.simbolos.splice(init_config.simbolos.indexOf(o), 1); //Eliminar el elemento current
                }
            });
            item.destroy();
        }
        else {
            debugger*/
            item.bringToTop();
        //}
            
    }
}

function Nombre(item) {
    text.text = 'Nombre de diente: ' + item.data.Nombre;
}

function obtenerNombre(i, y) {
    var cuadrante = obtenerCuadrante(i, y);
    var numero = obtenerNumero(i);
    return '' + cuadrante + '' + numero
}

function obtenerCuadrante(i, y) {
    // <= >=
    switch (y) {
        case 1:
            if (i >= 1 && i <= 8)
                return 1;
            else if (i >= 9 && i <= 16)
                return 2;
            else
                return NaN;
        case 2:
            if (i >= 4 && i <= 8)
                return 5;
            else if (i >= 9 && i <= 13)
                return 6;
            else
                return NaN;
        case 3:
            if (i >= 4 && i <= 8)
                return 8;
            else if (i >= 9 && i <= 13)
                return 7;
            else
                return NaN;
        case 4:
            if (i >= 1 && i <= 8)
                return 4;
            else if (i >= 9 && i <= 16)
                return 3;
            else
                return NaN;
        default:
            return NaN;
    }
}

function obtenerNumero(i) {
    if (i >= 1 && i <= 8)
        return 9 - i;
    else if (i >= 9 && i <= 16)
        return i - 8;
    else
        return NaN;
}

function addDiente(i, y) {
    var x = 0;
    item = grupo_dientes.create(T * (xEmp - 1) + T * i, T * (y + yEmp - 1), 'item');
    dentText = game.add.text(T * (xEmp - 1) + T * i + 25, T * (y + yEmp - 1) + T + 1, '');

    dentText.anchor.setTo(0.5);
    dentText.font = 'Arial';
    dentText.fontSize = 10;

    item.data = {
        tipo: "D",
        Nombre: obtenerNombre(i, y),
        x: item.x,
        y: item.y
    };

    init_config.dientes.push(item.data);

    dentText.text = item.data.Nombre;
    item.inputEnabled = true;

    item.input.enableSnap(T, T, false, true);

    /*Cargando de la BD*/
    init_config.simbolos.forEach(o => {
        if (item.data.Nombre === o.sNombreDiente) {
            var tmpItem = grupo_simbolos.create(T * (xEmp - 1) + T * i, T * (y + yEmp - 1), o.sDescripcion);
            tmpItem.inputEnabled = true;
            tmpItem.input.enableDrag();
            tmpItem.input.enableSnap(T, T, false, true);
            tmpItem.events.onDragStop.add(ValidationDrop, this);
            tmpItem.data = o;
            tmpItem.data.sidentifier = uniqueID();
        }
    });
}

function addAbreviatura(i, y) {
    var x = 0;
    item = grupo_espacios.create(T * (xEmp + i - 1), T * (yEmp + y), 'espacio');
    //game.add.sprite(T * (xEmp + i - 1), T * (yEmp - 1), 'espacio');
    //game.add.sprite(T * (xEmp + i - 1), T * (yEmp + 4), 'espacio');
    var abs_y = (0 > y) ? -y : y;
    item.data = {
        tipo: "E",
        Nombre: "E" + i + "." + abs_y,
        x: item.x,
        y: item.y
    };

    init_config.espacios.push(item.data);

    item.inputEnabled = true;

    item.input.enableSnap(T, T, false, true);

    /*Cargando de la BD*/
    init_config.simbolos.forEach(o => {
        //debugger
        if (item.data.Nombre === o.sNombreDiente) {
            //debugger
            var tmpItem = grupo_simbolos.create(T * (xEmp + i - 1), T * (yEmp + y), o.sDescripcion);
            tmpItem.inputEnabled = true;
            tmpItem.input.enableDrag();
            tmpItem.input.enableSnap(T, T, false, true);
            tmpItem.events.onDragStop.add(ValidationDrop, this);
            tmpItem.data = o;
            tmpItem.data.sidentifier = uniqueID();
        }
    });

}


function uniqueID() {
    // Math.random should be unique because of its seeding algorithm.
    // Convert it to base 36 (numbers + letters), and grab the first 9 characters
    // after the decimal.
    return '_' + Math.random().toString(36).substr(2, 9);
};

/*function fixLocation(item) {

    // Move the items when it is already dropped.
    if (item.x < 90) {
        item.x = 90;
    }
    else if (item.x > 180 && item.x < 270) {
        item.x = 180;
    }
    else if (item.x > 360) {
        item.x = 270;
    }

}*/
