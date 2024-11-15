import { useFormik } from "formik";
import { FaArrowRight } from "react-icons/fa";
import { loginInitValues, loginValidationSchema } from "../forms";
import { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import { useAuthStore } from "../store";
import { Loading } from "../../../shared/components";

export const LoginPage = () => {
  const navigate = useNavigate();
  const [loading, setLoading] = useState(false);
  const isAuthenticated = useAuthStore((state) => state.isAuthenticated);
  const login = useAuthStore((state) => state.login);
  const error = useAuthStore((state) => state.error);
  const message = useAuthStore((state) => state.message);

  useEffect(() => {
    if (isAuthenticated) {
      navigate("/menu/inicio");
    }
  }, [isAuthenticated, navigate]);

  const formik = useFormik({
    initialValues: loginInitValues,
    validationSchema: loginValidationSchema,
    validateOnChange: true,
    onSubmit: async (formValues) => {
      setLoading(true);
      await login(formValues);
      setLoading(false);
    },
  });

  if (loading) {
    return <Loading />;
  }

  return (
    <div className="flex min-h-screen flex-col-reverse md:flex-row">
      {/* Lado izquierdo: Mensaje de bienvenida con imagen de fondo */}
      <div
        className="flex w-full items-center justify-center bg-cover bg-center p-8 text-primary-foreground md:w-1/2"
        style={{
          backgroundImage:
            "url('https://img.freepik.com/foto-gratis/fondo-abstracto-tecnologia-lineas-particulas_53876-104054.jpg')",
        }}
      >
        <div className="max-w-md space-y-4 text-center">
          <h2 className="text-3xl font-bold text-white">Partidas Contables</h2>
          <p className="text-lg text-white">
            Inicia sesión para acceder a tu cuenta y disfrutar de nuestros
            servicios.
          </p>
        </div>
      </div>

      {/* Lado derecho: Formulario de inicio de sesión */}
      <div className="flex w-full items-center bg-gray-200 justify-center bg-background p-8 md:w-1/2">
        <div className="w-full max-w-md space-y-8 bg-white/90 backdrop-blur-lg shadow-xl p-8 rounded-xl z-10">
          <h1 className="font-bold text-center text-3xl text-blue-500 mb-6">
            Iniciar sesión
          </h1>
          {error && (
            <span className="p-4 block bg-red-500 text-white text-center rounded-t-lg mb-4">
              {message}
            </span>
          )}
          <form onSubmit={formik.handleSubmit} className="space-y-6">
            <div className="space-y-4">
              <div className="space-y-2">
                <label
                  className="font-semibold text-sm text-gray-600 block"
                  htmlFor="email"
                >
                  Correo electrónico
                </label>
                <input
                  type="email"
                  id="email"
                  name="email"
                  value={formik.values.email}
                  onChange={formik.handleChange}
                  className="border border-gray-300 rounded-lg px-4 py-3 w-full text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                />
                {formik.touched.email && formik.errors.email && (
                  <div className="text-red-500 text-xs mt-1">
                    {formik.errors.email}
                  </div>
                )}
              </div>
              <div className="space-y-2">
                <label
                  className="font-semibold text-sm text-gray-600 block"
                  htmlFor="password"
                >
                  Contraseña
                </label>
                <input
                  type="password"
                  id="password"
                  name="password"
                  value={formik.values.password}
                  onChange={formik.handleChange}
                  className="border border-gray-300 rounded-lg px-4 py-3 w-full text-sm focus:outline-none focus:ring-2 focus:ring-blue-500"
                />
                {formik.touched.password && formik.errors.password && (
                  <div className="text-red-500 text-xs mt-1">
                    {formik.errors.password}
                  </div>
                )}
              </div>
            </div>
            <button
              type="submit"
              disabled={loading}
              className="w-full py-3 rounded-lg text-white bg-blue-600 hover:bg-blue-700 focus:outline-none focus:ring-4 focus:ring-blue-500 transition duration-200 flex items-center justify-center text-sm font-semibold"
            >
              <span className="inline-block mr-2">Ingresar</span>
              <FaArrowRight className="w-5 h-5 inline-block" />
            </button>
          </form>
        </div>
      </div>
    </div>
  );
};
