import React from 'react'
import { Alert } from '../pages/Dashboard/dashboard';

function alertToast(obj: Alert) {
    const background = {
    info: "#3498db",
    success: "#2ecc71",
    error: "#e74c3c",
    warning: "#f1c40f"
  }[obj.status] || "#3498db";

  return (
    <div className={`toast`} style={{background}}>{obj.status} - {obj.message} - {obj.timestamp}</div>
  )
}

export default alertToast