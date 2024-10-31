describe('template spec', () => {
  it('passes', () => {
      cy.visit('https://localhost:7029/')
      cy.get('#NombreUsuario').type('FER');
      cy.get('#Password').type('ferjos123');
      cy.get('.btn').click();
  })
})