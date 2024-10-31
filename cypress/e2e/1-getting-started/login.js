describe('Prueba de inicio de sesión', () => {
    it('Iniciar sesión con credenciales válidas', () => {
        cy.visit('https://localhost:7029/'); // Visita la página principal
        <reference types="cypress" />
        // Ingresa las credenciales
        cy.get('input[name="email"]').type('Fer'); // Cambia esto al correo del usuario
        cy.get('input[name="password"]').type('ferjos123'); // Cambia esto a la contraseña del usuario

        // Haz clic en el botón de inicio de sesión
        cy.get('button[type="submit"]').click();

        // Verifica que la redirección o el mensaje de éxito esté presente
        cy.url().should('include', 'https://localhost:7029/Home/Camareros'); // Cambia esto a la URL esperada después del inicio de sesión
    });
});
