import { useState, ChangeEvent, FormEvent } from "react";
import { useNavigate } from "react-router-dom";
import { BACKEND_URL } from "../../assets/globalVariables";
import styles from "./auth.module.css";

interface FormData {
  password: string;
  email: string;
}

export default function LogIn() {
  const navigate = useNavigate();
  const [loadingLogin, setLoadingLogin] = useState<boolean>(false);
  const [formData, setFormData] = useState<FormData>({
    email: "",
    password: "",
  });

  const handleChange = (e: ChangeEvent<HTMLInputElement>) => {
    setFormData({ ...formData, [e.target.name]: e.target.value });
  };

  const handleLogIn = async (e: FormEvent<HTMLFormElement>) => {
    e.preventDefault();
    setLoadingLogin(true);
    const response = await fetch(`${BACKEND_URL}/auth/login`, {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({ formData }),
    });
    const data = await response.json();

    if (response.ok) {
      setLoadingLogin(false);
      localStorage.setItem("auth", data)
      navigate("/dashboard");
    } else {
    }
  };

  return (
    <main>
      <h1>Welcome back</h1>
      <form onSubmit={handleLogIn}>
        <div className={styles.formGroup}>
          <label htmlFor="email">Email</label>
          <input
            type="email"
            id="email"
            name="email"
            required
            value={formData.email}
            onChange={handleChange}
          />
        </div>
        <div className={styles.formGroup}>
          <label>Password</label>
          <input
            id="password"
            type="password"
            name="password"
            required
            value={formData.password}
            onChange={handleChange}
          />
        </div>
        <button type="submit"> Log in</button>
      </form>
    </main>
  );
}
