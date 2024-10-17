# Branch: `development`

Este branch (`development`) es la base para todos los cambios de desarrollo del proyecto. Aquí se integrarán todas las nuevas funcionalidades, mejoras y correcciones antes de pasar al branch principal (`main`).

## Flujo de trabajo

1. **Crear branches a partir de `development`:**  
   Todas las nuevas funcionalidades, correcciones de bugs, o mejoras deberán realizarse en branches individuales que se creen desde el branch `development`. Asegúrate de usar nombres descriptivos para los branches, por ejemplo:
### Para Features
~~~
feature/nueva-funcionalidad
~~~
### Arreglo de bugs
~~~
bugfix/correccion-error
~~~

### Mejoras
~~~
improvement/mejora-rendimiento
~~~

2. **Revisión de código (Pull Requests):**  
   Antes de hacer un merge al branch `development`, todos los branches deben pasar por un proceso de revisión de código (pull request). Un revisor debe aprobar el código y verificar que cumpla con los estándares del proyecto.

3. **Commit y merge al branch `development`:**  
   - Una vez revisado y aprobado, el branch puede ser mergeado a `develop`.
   - Asegúrate de hacer squash commits si es necesario, para mantener un historial limpio.

4. **Integración del branch `development` en `main`:**  
   Al finalizar el desarrollo del proyecto, el branch `develop` será mergeado en el branch `main`. Este paso marca el cierre del desarrollo activo y puede coincidir con la entrega oficial del proyecto.

## Reglas para commits

- Los commits directos en `development` están desaconsejados. Todo debe pasar por un branch dedicado y una revisión previa.
- Los mensajes de commit deben ser claros y descriptivos. Utiliza una convención para los mensajes, por ejemplo:
  - `feat:` para nuevas características
  - `fix:` para correcciones de errores
  - `chore:` para tareas de mantenimiento o cambios sin impacto funcional

## Merging

- Los branches **nunca deben hacer merge directo en `main`**. Solo se permitirá el merge desde `development` hacia `main` cuando el proyecto esté finalizado.
- Asegúrate de que todos los cambios en `development` sean estables y estén completamente probados antes de hacer el merge final a `main`.
