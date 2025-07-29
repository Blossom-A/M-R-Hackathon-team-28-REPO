import React, { useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";

const AuthGuard = ({ component }: any) => {
    const [status, setStatus] = useState(false);
  const navigate = useNavigate();

  useEffect(() => {
    checkToken();
  }, [component]);

  const checkToken = async () => {
    try {
      let user = true;
      if (!user) {
        navigate(`/auth`);
      }
      setStatus(true);
      return;
    } catch (error) {
      navigate(`/auth`);
    }
  }

  return status ? <>{component}</> : <></>;
};

export default AuthGuard;