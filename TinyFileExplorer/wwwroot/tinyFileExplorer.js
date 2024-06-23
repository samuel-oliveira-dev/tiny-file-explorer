document.addEventListener('DOMContentLoaded', function () {

   

    // Seleciona todos os elementos com a classe 'arrow'
    const arrows = document.querySelectorAll('.arrow');

    // Adiciona um event listener de clique a cada 'arrow'
    arrows.forEach(function (arrow) {
        arrow.addEventListener('click', function (event) {
            event.stopPropagation();

            // Seleciona o elemento pai com a classe 'folder'
            const folder = arrow.closest('.folder');
            const state = folder.getAttribute('state');

            if (state === 'open') {
                // Esconde os elementos filhos
                const childUl = folder.querySelector('ul');
                if (childUl) {
                    childUl.style.display = 'none';
                }
                folder.setAttribute('state', 'closed');

                // Atualiza o ícone da seta
                arrow.classList.remove('arrow-down');
                arrow.classList.add('arrow-right');
            } else if (state === 'closed') {
                // Mostra os elementos filhos
                const childUl = folder.querySelector('ul');
                if (childUl) {
                    childUl.style.display = 'block';
                }
                folder.setAttribute('state', 'open');

                // Atualiza o ícone da seta
                arrow.classList.remove('arrow-right');
                arrow.classList.add('arrow-down');
            }
        });
    });
});