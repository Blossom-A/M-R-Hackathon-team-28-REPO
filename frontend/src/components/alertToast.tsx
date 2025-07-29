import React from 'react'

function alertToast({ message = "This is a notification!", type = "info" }) {
    const background = {
    info: "#3498db",
    success: "#2ecc71",
    error: "#e74c3c",
    warning: "#f1c40f"
  }[type] || "#3498db";

  return (
    <div className={`toast`} style={{background}}>{message}</div>
  )
}

export default alertToast