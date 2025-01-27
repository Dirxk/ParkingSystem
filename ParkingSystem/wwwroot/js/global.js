// Mostrar el modal
function mostrarModal() {
    var modal = new bootstrap.Modal(document.getElementById('staticBackdrop'));
    modal.show();
}

// Ocultar el modal
function ocultarModal() {
    var modal = bootstrap.Modal.getInstance(document.getElementById('staticBackdrop'));
    modal.hide();
}